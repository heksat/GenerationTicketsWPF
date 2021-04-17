using System;
using System.Collections.Generic;
using System.Text;
using GenerationTicketsWPF.Pages;
using System.Linq;
namespace GenerationTicketsWPF.Models
{
    class DbInteraction
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
        public SByte AddUser(Worker newuser, List<string> choicelistdisname)
        {
            SByte check = 0;
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                db.Workers.Add(newuser);
                var ias= db.Workers.Where(x => x == newuser).Select(x => x.WorkerId).FirstOrDefault();
                // db.SaveChanges();
                if (newuser.RoleId == 2) //teacher
                {
                    int id = db.Workers.Where(x => x.WorkerLogin == newuser.WorkerLogin).Select(x => x.WorkerId).FirstOrDefault();
                    if (id != 0)
                    {
                        foreach (var i in choicelistdisname)
                        {
                            var tempid = (int)db.Disciplines.Where(x => x.DisciplineName == i).Select(x => x.DisciplineId).FirstOrDefault();
                            if (tempid != 0)
                                db.Teachings.Add(new Teaching() { WorkerId = id, DisciplineId = tempid });
                            else
                                check = -2;
                        }
                    }
                    else
                        check = -3;
                }
                if (check==0)
                    db.SaveChanges();
                return check;
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
