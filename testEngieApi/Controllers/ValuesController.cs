using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testEngieApi.Models;

namespace testEngieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ValuesController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] ParametrosEntrada value)
        {
            int numberList = CheckNumberList(value.ElementNumber);
            if (numberList == 0)
            {
                return BadRequest(new { message = "The ElementNumber should be an integer between 2 and 10^4" });
            }

            string list = CheckList(value.ListNumbers, numberList);
            switch (list)
            {
                case "error":
                    return BadRequest(new { message = "The ListNumbers has a problem. Remember write integers between 1 and 100, " +
                        "separated with a space between them"});
                case "notPosible":
                    return NotFound(new { message = "With this numbers, isn't posible obtain a result divisible for 101 :(" });
                default:
                    return Ok(new { message = $"The operation for obtain a number divisible for 101 is: {list}   :)" });

            }
            
        }

        private static int CheckNumberList(string readline)
        {
            try
            {
                int numberList = Convert.ToInt32(readline);
                if (numberList < 2 || numberList > Math.Pow(10, 4))
                {
                    return 0;
                }
                return numberList;

            }
            catch (FormatException)
            {
                return 0;
            }
        }

        private static string CheckList(string listString, int numberList)
        {
            try
            {
                int[] list = listString.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
                if (list.Count() != numberList)
                {
                    return "error";
                }
                foreach(int element in list)
                {
                    if (element < 1 || element > 100){
                        return "error";
                    }
                }
                string preResult = list[0] + RecursiveCalc(list, list[0], 1);
                if (preResult.Equals(list[0].ToString()))
                {
                    return "notPosible";
                }
                return preResult;
            }
            catch (Exception)
            {
                return "error";
            }
        }

        private static string RecursiveCalc(int[] list, int current, int index)
        {
            current %= 101;
            if (index == list.Count())
                return current == 0 ? "" : null;
            String result = RecursiveCalc(list, current + list[index], index + 1);
            if (result != null)
                return "+" + list[index] + result;
            result = RecursiveCalc(list, current * list[index], index + 1);
            if (result != null)
                return "*" + list[index] + result;
            result = RecursiveCalc(list, current - list[index], index + 1);
            if (result != null)
                return "-" + list[index] + result;
            return null;
        }
    }
}
