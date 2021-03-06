﻿using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace HHSIB
{
    public partial class ibCalendar : System.Web.UI.Page
    {
        Hashtable schedule = new Hashtable();
         
        protected void Page_Load(object sender, EventArgs e)
        {
            GetSchedule();

            //Calendar1.Style.Add("position", "absolute");
            //Calendar1.Style.Add("left", "100px");
            //Calendar1.Style.Add("top", "300px");
            Calendar1.FirstDayOfWeek = FirstDayOfWeek.Sunday;
            Calendar1.NextPrevFormat = NextPrevFormat.ShortMonth;
            Calendar1.TitleFormat = TitleFormat.MonthYear;
            Calendar1.ShowGridLines = true;
            Calendar1.DayStyle.HorizontalAlign = HorizontalAlign.Left;
            Calendar1.DayStyle.VerticalAlign = VerticalAlign.Top;
            Calendar1.DayStyle.Height = new Unit(75);
            Calendar1.DayStyle.Width = new Unit(100);
            Calendar1.OtherMonthDayStyle.BackColor = System.Drawing.Color.WhiteSmoke;
            Calendar1.VisibleDate = Calendar1.TodaysDate; 
            
        }

        private void GetSchedule()
        {
            IBHhsDataContext db = new IBHhsDataContext();
            var results = from p in db.CalendarIBs
                          orderby p.IBDate
                          select p;
                        
            foreach (CalendarIB c in results)
            {                
                //schedule[c.IBDate.ToShortDateString()] = c.IBEVent.ToString().Trim();
                schedule[c.IBDate] = c.IBEVent.ToString().Trim();
            }


        }
         
        
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            Literal lit = new Literal();
            lit.Visible = true;
            lit.Text = "<br />";
            e.Cell.Controls.Add(lit);
            DateTime dt = e.Day.Date;
            
            //if (schedule[e.Day.Date.ToShortDateString()] != null)
                if (schedule[e.Day.Date] != null)
                {
                if (!e.Day.IsOtherMonth)
                {
                    Label lbl = new Label();
                    lbl.Visible = true;
                    lbl.Text = (string)schedule[e.Day.Date];
                    e.Cell.Controls.Add(lbl);
                    e.Cell.BackColor = System.Drawing.Color.Violet;
                    e.Cell.CssClass = "detail_body";
                }                
            }
            
        }

        
    }
        
}
