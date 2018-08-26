using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using WebApplicationSorter.Models;

namespace WebApplicationSorter.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult Sorter(HttpPostedFileBase upload)
        {
            DataFile allData = new DataFile();
            int count = 0;

            string fileName = Path.GetFileName(upload.FileName);
            upload.SaveAs(Server.MapPath("~/App_Data/" + fileName));


            //построчное чтение и сохранение в лист
            using (StreamReader sr = new StreamReader(Server.MapPath("~/App_Data/" + fileName)))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    RawData rawData = new RawData();
                    string[] words = line.Split(new char[] { '.' });
                    rawData.Num = Convert.ToInt32(words[0]);
                    rawData.Text = Convert.ToString(words[1]);
                    allData.list.Add(rawData);
                    count++;
                }
            }
            ////сортировка по текстовой части
            RawData temp;
            for (int i = 0; i < allData.list.Count - 1; i++)
            {
                for (int j = i + 1; j < allData.list.Count; j++)
                {
                    int res = String.Compare(allData.list[i].Text, allData.list[j].Text);
                    if (res > 0)
                    {
                        temp = allData.list[i];
                        allData.list[i] = allData.list[j];
                        allData.list[j] = temp;
                    }
                }
            }
            // сортировка по численной части
            for (int i = 0; i < allData.list.Count - 1; i++)
            {
                for (int j = i + 1; j < allData.list.Count; j++)
                {
                    int res = String.Compare(allData.list[i].Text, allData.list[j].Text);
                    if (res == 0)
                    {
                        if (allData.list[i].Num > allData.list[j].Num)
                        {
                            temp = allData.list[i];
                            allData.list[i] = allData.list[j];
                            allData.list[j] = temp;
                        }

                    }
                }
            }
            //сохранение в файл
            using (StreamWriter sw = new StreamWriter(Server.MapPath("~/App_Data/SortedText.txt")))
            {
                foreach (var item in allData.list)
                {
                    sw.WriteLine(item.Num + "." + item.Text);
                }
            }

            ViewBag.Message = count;
            ViewBag.Path = "~/App_Data/SortedText.txt";
            return View("Sorted");
        }


        public ActionResult Index()
        {
            return View();
        }

       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}