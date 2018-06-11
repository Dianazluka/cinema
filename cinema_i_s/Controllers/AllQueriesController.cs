using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cinema_i_s.Classes;
using cinema_i_s.Models;
using Npgsql;
using NpgsqlTypes;

namespace cinema_i_s.Controllers
{
    public class AllQueriesController : Controller
    {
        [Authorize]
        // GET: AllQueries
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Query1(string say)
        {
            List<Query1Data> result;
            using (var db = new cinema_inform_sistemEntities11())
            {
                var param1 = new NpgsqlParameter("@say", say)
                {
                    NpgsqlDbType = NpgsqlDbType.Text
                };
               

                string sql = @"SELECT * 
                               FROM film
                                WHERE genre='Драма' 
                                GROUP BY film_id   || @say || '%'";
                result = db.Database.SqlQuery<Query1Data>(sql, param1).ToList();
            }

            TempData["result"] = result;

            return RedirectToAction("Query1Result");
        }
        [HttpPost]
        public ActionResult Query2()
        {
            List<Query2Data> result;
            using (var db = new cinema_inform_sistemEntities11())
            {
                string sql = @"SELECT f.name_film, f.year, f.age_limit  FROM film f WHERE f.name_film NOT LIKE '%День%' UNION SELECT f.name_film, f.year, f.age_limit  FROM film f WHERE f.film_id = 3; ;";
                result = db.Database.SqlQuery<Query2Data>(sql).ToList();
            }

            TempData["result"] = result;

            return RedirectToAction("Query2Result");
        }
        [HttpPost]
        public ActionResult Query3(string letter)
        {
            List<Query3Data> result;
            using (var db = new cinema_inform_sistemEntities11())
            {
                var param1 = new NpgsqlParameter("@letter", letter.Trim())
                {
                    NpgsqlDbType = NpgsqlDbType.Varchar
                };
             
                string sql = @"SELECT * FROM associate a 	INNER JOIN seance s 	ON s.id_assiciate = a.associate_id WHERE a.associate_id = ANY  	(SELECT f.film_id 	FROM film f 	WHERE f.name_film LIKE '' || @letter || '%'))";
                result = db.Database.SqlQuery<Query3Data>(sql,param1).ToList();
            }

            TempData["result"] = result;

            return RedirectToAction("Query3Result");
        }
        [HttpPost]
        public ActionResult Query4()
        {
            List<Query4Data> result;
            using (var db = new cinema_inform_sistemEntities11())
            {
                string sql = @"SELECT s.date, s.time, h.name_hall, h.capacity FROM seance s RIGHT OUTER JOIN hall h ON s.id_hall = h.holl_id WHERE s.id_hall >(SELECT MIN(length(name_hall)) 	FROM hall 	WHERE holl_id>1);";
                result = db.Database.SqlQuery<Query4Data>(sql).ToList();
            }

            TempData["result"] = result;

            return RedirectToAction("Query4Result");
        }
        public ActionResult Query5( DateTime date1)
        {
            List<Query5Data> result;
            using (var db = new cinema_inform_sistemEntities11())
            {
                
                var param = new NpgsqlParameter("@date1", date1)
                {
                    NpgsqlDbType = NpgsqlDbType.Date
                };
               

                string sql = @"SELECT   COUNT(*) FROM seance s, ticket t WHERE (s.date='2018-03-05') AND (s.seance_id = t.seance_id);";
                result = db.Database.SqlQuery<Query5Data>(sql, param).ToList();
            }

            TempData["result"] = result;

            return RedirectToAction("Query5Result");
        }
        public ActionResult Query6(DateTime date1, DateTime date2)
         {
             List<Query6Data> result;
             using (var db = new cinema_inform_sistemEntities11())
             {

                 var param2 = new NpgsqlParameter("@date1", date1)
                 {
                     NpgsqlDbType = NpgsqlDbType.Date
                 };
                 var param3 = new NpgsqlParameter("@date2", date2)
                 {
                     NpgsqlDbType = NpgsqlDbType.Date
                 };

                 string sql = @"SELECT SUM(id_ticket) FROM ticket_selling WHERE date_of_sale  BETWEEN @date1 AND @date2";
                 result = db.Database.SqlQuery<Query6Data>(sql, param2, param3).ToList();
             }

             TempData["result"] = result;

             return RedirectToAction("Query6Result");
         }     
    }
}