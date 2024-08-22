using Microsoft.Ajax.Utilities;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace GameDineHub
{
    /// Handles HTML Tags and attributes.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Returns an unordered list (ul element) of validation messages that utilizes bootstrap markup and styling.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="alertType">The alert type styling rule to apply to the summary element.</param>
        /// <param name="heading">The optional value for the heading of the summary element.</param>
        /// <returns>HtmlString.</returns>
        public static HtmlString ValidationBootstrap(this HtmlHelper htmlHelper, string alertType = "danger", string heading = "")
        {
            if (htmlHelper.ViewData.ModelState.IsValid)
                return new HtmlString(string.Empty);
            var sb = new StringBuilder();
            sb.AppendFormat("<div class=\"alert alert-{0} alert-dismissible fade show\" role=\"alert\">", alertType);
            sb.Append("<button class=\"close\" data-dismiss=\"alert\" ><span aria-hidden=\"true\"><i class=\"fal fa-times-square\"></i></span></button>");

            if (!heading.IsNullOrWhiteSpace())
            {
                sb.AppendFormat("<h4 class=\"alert-heading\">{0}</h4>", heading);
            }

            sb.Append(htmlHelper.ValidationSummary());
            sb.Append("</div>");

            return new HtmlString(sb.ToString());
        }
    }
}