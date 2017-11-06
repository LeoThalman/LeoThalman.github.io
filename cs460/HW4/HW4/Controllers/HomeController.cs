using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HW4.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = "Homework 4 Home Page";
            return View();
        }

        /// <summary>
        /// Gets the View for Page1 and loads it
        /// </summary>
        /// <returns>loads Page1.html</returns>
        public ActionResult Page1()
        {
            ViewBag.Title = "Temperature Conversion";
            return View();
        }

        /// <summary>
        /// Checks if the temp is empty, 
        /// parses temp into a double and converts it to Celsius or Fahrenheit depending
        /// on what was entered in tempType, if neither c or f pass an error message
        /// </summary>
        /// <returns>The new page with the new temperature</returns>
        public ActionResult Page1Text()
        {
        if (Request.Form["temp"] != null && Request.Form["tempType"] != null)
        {
            string temp = Request.Form["temp"];
            string tempType = Request.Form["tempType"];
            ViewBag.Title = "Page 1";
            tempType = tempType.ToLower();
            if (Double.TryParse(temp, out double nTemp))
            {
                if (tempType.Equals("c"))
                {
                    nTemp = (nTemp - 32) / 1.8;
                    nTemp = Math.Round(nTemp, 2);
                    ViewBag.Message = "Your temp is " +  nTemp.ToString();

                }
                else if (tempType.Equals("f"))
                {
                    nTemp = (nTemp * 1.8) + 32;
                    nTemp = Math.Round(nTemp, 2);
                    ViewBag.Message = "Your temp is " + nTemp.ToString();
                }
                else
                {
                        ViewBag.Message = "Invalid Temperature type, please enter C or F";
                }
            }
                else
                {
                    ViewBag.Message = "Please enter a Temp number";
                }
        }
        return View();
        }

        /// <summary>
        /// Returns the base page for Time conversion
        /// </summary>
        /// <returns>The base page for Time conversion</returns>
        [HttpGet]
        public ActionResult Page2()
        {
            ViewBag.Title = "Time Conversion";
            return View();
        }

        /// <summary>
        /// Takes the form from Page2 and pulls out the time and timeType fields
        /// checks if time is a double and if time type is H or M, if true
        /// on both accounts then converts time from hours to minutes or vice versa.
        /// if false then returns an error message asking for a value for time
        /// or H or M.
        /// </summary>
        /// <param name="form">the form containing time and timeType</param>
        /// <returns>the view with an error message or the converted time</returns>
        [HttpPost]
        public ActionResult Page2(FormCollection form)
        {
            string time = Convert.ToString(form["time"]);
            string timeType = Convert.ToString(form["timeType"]);
            ViewBag.Title = "Hour/Minute Converter";
            timeType = timeType.ToLower();
            if (Double.TryParse(time, out Double nTime))
            {
                if(timeType.Equals("h"))
                {
                    nTime = nTime / 60;
                    nTime = Math.Round(nTime, 2);
                    ViewBag.Message = "Your time is " + nTime.ToString();
                }
                else if (timeType.Equals("m"))
                {
                    nTime = nTime * 60;
                    nTime = Math.Round(nTime, 2);
                    ViewBag.Message = "Your time is " + nTime.ToString();
                }
                else
                {
                    ViewBag.Message = "Please Enter H or M to convert to" + time.ToString();
                }
            }
            else
            {
                ViewBag.Message = "Please enter a time";
            }
            return View();
        }

        /// <summary>
        /// Returns the base page for Loan Calculator
        /// </summary>
        /// <returns>base page for Loan Calculator</returns>
        [HttpGet]
        public ActionResult Page3()
        {
            ViewBag.Title = "Loan Calculator";
            return View();
        }

        /// <summary>
        /// Takes the principal payment, interest rate, and term length
        /// and then checks if they're null, if so returns an error message.
        /// Else it takes the three parameters and then uses a loan rate formula to
        /// figure out the monthly payments. Sends the monthly payments to the view and also
        /// fiugres out the total payment and sends that to the view.
        /// </summary>
        /// <param name="principal">principal payment</param>
        /// <param name="interest">yearly interest rate</param>
        /// <param name="termLength">term length in months</param>
        /// <returns>The view with the error message or payments</returns>
        [HttpPost]
        public ActionResult Page3(double? principal, double? interest, double? termLength)
        {
            ViewBag.Title = "Loan Calculator";
            ViewBag.errMessage = null;
            if (LoanCheck(principal, interest, termLength))
            {
                ViewBag.errMessage = "Please enter a value in all 3 fields";
            }
            else
            {
                double prin = (double)principal;
                double iRate = (double)interest;
                double numP = (double)termLength;
                iRate = iRate * .01;
                iRate = iRate / 12;
                double payment = 0;
                payment = (iRate * prin) / (1 - Math.Pow((1 + iRate), -numP));
                double total = payment * numP;
                total = Math.Round(total, 2);
                payment = Math.Round(payment, 2);
                ViewBag.mPay = "Your monthly payment is $" + payment.ToString();
                ViewBag.tPay = "Your total payment is $" + total.ToString();
            }
            return View();
        }

        /// <summary>
        /// Error checking for loan calculator, takes 3 parameters from
        /// page 3 and checks to see if any are null, if so returns true else
        /// returns false
        /// </summary>
        /// <param name="principal">principal payment</param>
        /// <param name="interest">interest rate</param>
        /// <param name="termLength">term length</param>
        /// <returns>true if any value is null, else false</returns>
        private Boolean LoanCheck(double? principal, double? interest, double? termLength)
        {
            Boolean rtn = false;
            if(principal == null || interest == null || termLength == null)
            {
                rtn = true;
            }
            return rtn;
        }
    }
}