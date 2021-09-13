
/* flatpickr v4.4.3, @license MIT */
(function (global, factory)
{
    typeof exports === 'object' && typeof module !== 'undefined' ? factory(exports) :
        typeof define === 'function' && define.amd ? define(['exports'], factory) :
            (factory((global.en = {})));
}(this, (function (exports)
{
    'use strict';

    var fp = typeof window !== "undefined" && window.flatpickr !== undefined ? window.flatpickr : {
        l10ns: {}
    };
    var English = {
        weekdays: {
            shorthand: ["So", "Mo", "Di", "Mi", "Do", "Fr", "Sa"],
            longhand: [
                "Sunday",
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday",
            ]
        },
        months: {
            shorthand: [
                "Jan",
                "Feb",
                "Mar",
                "Apr",
                "May",
                "Jun",
                "Jul",
                "Aug",
                "Sep",
                "Oct",
                "Nov",
                "Dec",
            ],
            longhand: [
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December",
            ]
        },
        ordinal: function (nth)
        {
            var s = nth % 100;
            if (s > 3 && s < 21)
                return "th";
            switch (s % 10)
            {
                case 1:
                    return "st";
                case 2:
                    return "nd";
                case 3:
                    return "rd";
                default:
                    return "th";
            }
        },
        firstDayOfWeek: 1,
        weekAbbreviation: "Wk",
        rangeSeparator: " to ",
        scrollTitle: "Scroll to increment",
        toggleTitle: "Click to toggle"
    };
    fp.l10ns.en = English;
    var en = fp.l10ns;

    exports.English = English;
    exports.default = en;

    Object.defineProperty(exports, '__esModule', { value: true });
})));
