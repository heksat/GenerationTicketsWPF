using System;
using System.Collections.Generic;
using System.Text;
using GenerationTicketsWPF.Pages;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using GenerationTicketsWPF;
namespace GenerationTicketsWPF.Models
{


    public class DbInteraction
    {
        public Worker Auth(string login,string password)
        {
            using (var db = new GenerationTicketsContext(Config.Options))
            {
               return db.Workers.Where(el => el.WorkerLogin.Equals(login) && el.WorkerPassword.Equals(password)).FirstOrDefault();
            }
        }
        public object ListTickets()
        {
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                return (from p in db.Tickets join c in db.Tasks on p.TaskId equals c.TaskId select new { p.TicketId, p.TaskNumber, c.TaskDecryption }).ToList();
            }
        }
        public List<Discipline> GetDiscipList()
        {
            using (var db = new GenerationTicketsContext(Config.Options)) 
            {
                return (from p in db.Disciplines
                        join c in db.Teachings on p.DisciplineId equals c.DisciplineId
                        where c.WorkerId == Config.User.WorkerId
                        select p ).ToList();
            }
        }
        public List<Level> GetLevels()
        {
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                return db.Levels.Select(x => x).ToList();
            }
        }
        public bool UpdatePassword(string pass)
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
        public List<string> GetAllowDisciplines()
        {
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                return (from p in db.Disciplines
                         join c in db.Teachings on p.DisciplineId equals c.DisciplineId
                         where c.WorkerId == Config.User.WorkerId
                         select p.DisciplineName).ToList();
            }
        }
        public List<string> GetDisciplinesDecryption()
        {
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                return db.Disciplines.Select(x => x.DisciplineName).ToList();
            }
        }
        public List<Discipline> GetDisciplines()
        {
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                return db.Disciplines.Select(x => x).ToList();
            }
        }


        public SByte AddUser(Worker newuser, List<Discipline> choicelistdisname)
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
                
                db.SaveChanges();
                return check;
            }
        }
        public void UpdateUser(Worker worker)
        {
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                db.Entry(worker).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public List<Task> GetTasks(string disp = null, string lvl = null)
        {
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                List<Task> templist;
                if (disp == null || lvl == null)
                {
                    templist =  db.Tasks.ToList();
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
        public bool PullTickets(List<Ticket> newlist)
        {
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                db.Tickets.RemoveRange(db.Tickets);
                db.Tickets.AddRange(newlist);
                db.SaveChanges();
            }
            return true;
        }
        public List<Chairman> GetChairmanList(string disp)
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
        public Specialty GetSpectoDisp(string disp)
        {
            using (var db = new GenerationTicketsContext(Config.Options))
            {
               return (from c in db.Disciplines
                     join p in db.Specialties on c.SpecialtyId equals p.SpecialtyId
                     where c.DisciplineName == disp
                    select p).FirstOrDefault();
            }
        }
        public List<Ticket> GetTickets()
        {
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                return db.Tickets.ToList();
            }
        }
        public string GetDispfromTickets()
        {
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                return db.Disciplines.Where(x => x.DisciplineId == (db.Tickets.Select(x=>x.DisciplineId).FirstOrDefault())).Select(x=>x.DisciplineName).FirstOrDefault();
               // from p in db.Tickets join c in db.Tasks on p.TaskId equals c.TaskId select c.DisciplineId
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
