using System;
using System.Collections.Generic;
using System.Text;
using GenerationTicketsWPF.Pages;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using GenerationTicketsWPF;
using System.Windows;

namespace GenerationTicketsWPF.Models
{


    public class DbInteraction
    {
        public Worker Auth(string login,string password)
        {
            try
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    return db.Workers.Where(el => el.WorkerLogin.Equals(login) && el.WorkerPassword.Equals(password)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public object ListTickets()
        {
            try
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    return (from p in db.Tickets join c in db.Tasks on p.TaskId equals c.TaskId select new { p.TicketId, p.TaskNumber, c.TaskDecryption }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public List<Discipline> GetDiscipList()
        {
            try
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    return (from p in db.Disciplines
                            join c in db.Teachings on p.DisciplineId equals c.DisciplineId
                            where c.WorkerId == Config.User.WorkerId
                            select p).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Discipline>(0);
            }
        }
        public List<Level> GetLevels()
        {
            try
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    return db.Levels.Select(x => x).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Level>(0);

            }
        }
        public bool UpdatePassword(string pass)
        {
            try
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    bool check = false;
                    var user = db.Workers.Where(x => x.WorkerId == Config.User.WorkerId).Select(x => x).FirstOrDefault();
                    if (user != null)
                    {
                        user.WorkerPassword = pass;
                        db.SaveChanges();
                        check = true;
                    }
                    return check;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public List<string> GetAllowDisciplines()
        {
            try
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    return (from p in db.Disciplines
                            join c in db.Teachings on p.DisciplineId equals c.DisciplineId
                            where c.WorkerId == Config.User.WorkerId
                            select p.DisciplineName).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<string>(0);
            }
        }
        public List<string> GetDisciplinesDecryption()
        {
            try
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    return db.Disciplines.Select(x => x.DisciplineName).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<string>(0);
            }
        }
        public List<Discipline> GetDisciplines()
        {
            try
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    return db.Disciplines.Select(x => x).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Discipline>(0);
            }
        }


        public SByte AddUser(Worker newuser, List<Discipline> choicelistdisname)
        {
            try
            {
                SByte check = 0;
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    db.Workers.Add(newuser);
                    if (newuser.RoleId == 2) //teacher
                    {
                        choicelistdisname.ForEach((i) =>
                             db.Entry(new Teaching() { Worker = newuser, Discipline = i }).State = EntityState.Added);
                    }
                   // else
                   // {
                    //    if (newuser.RoleId == 1)
                    //    {
                    //        var listdis = db.Disciplines.ToList();
                    //        listdis.ForEach((i) =>
                    //         db.Entry(new Teaching() { Worker = newuser, Discipline = i }).State = EntityState.Added);
                    //    }
                    //}
                    db.SaveChanges();
                    return check;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public void UpdateUser(Worker worker)
        {
            try
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    db.Entry(worker).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public List<Task> GetTasks(string disp = null, string lvl = null)
        {
            try
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    List<Task> templist;
                    if (disp == null || lvl == null)
                    {
                        templist = db.Tasks.ToList();
                    }
                    else
                    {
                        templist = db.Tasks.Where(y =>
                                (y.DisciplineId == (db.Disciplines.Where(x => x.DisciplineName == disp).Select(x => x.DisciplineId).FirstOrDefault()))
                                && (y.Level.LeverDecryption == lvl)
                                ).ToList();//Select(x => new { IDTask = x.TaskId, TypeTask = x.TypesTaskId });
                    }
                    return templist;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Task>(0);

            }

        }
        public bool PullTickets(List<Ticket> newlist)
        {
            try
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    db.Tickets.RemoveRange(db.Tickets);
                    db.Tickets.AddRange(newlist);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;
        }
        public List<Chairman> GetChairmanList(string disp)
        {
            try
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    return (from p in db.Chairmans
                            join c in db.Specialties on p.ChairmanId equals c.ChairmanId
                            join x in db.Disciplines on c.SpecialtyId equals x.SpecialtyId
                            join t in db.Teachings on x.DisciplineId equals t.DisciplineId
                            where (t.WorkerId == Config.User.WorkerId) && (disp == x.DisciplineName)//Config.User.WorkerId
                            select p).Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Chairman>(0);
            }
        }
        public Specialty GetSpectoDisp(string disp)
        {
            try
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    return (from c in db.Disciplines
                            join p in db.Specialties on c.SpecialtyId equals p.SpecialtyId
                            where c.DisciplineName == disp
                            select p).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public List<Ticket> GetTickets()
        {
            try
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    return db.Tickets.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Ticket>(0);
            }
        }
        public string GetDispfromTickets()
        {
            try
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    return db.Disciplines.Where(x => x.DisciplineId == (db.Tickets.Select(x => x.DisciplineId).FirstOrDefault())).Select(x => x.DisciplineName).FirstOrDefault();
                    // from p in db.Tickets join c in db.Tasks on p.TaskId equals c.TaskId select c.DisciplineId
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public void TaskAdd(Task task)
        {
            try
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    db.Tasks.Add(task);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

            //public List<T> GetListTable<T>()
            //{
            //    var test = (Type.GetTypeCode(typeof(Discipline)));
            //    using (var db = new GenerationTicketsContext(Config.Options)) {
            //        switch(Type.GetTypeCode(typeof(T)))
            //        {
            //            case test: return db.Disciplines.Select(x => x).ToList();
            //            default: return null;
            //        };

            //    }
            //}
        }
}
