import { compareDates, compareTimes, createDateFormatter, } from "../utils/dates";
function minMaxTimePlugin(config) {
    if (config === void 0) { config = {}; }
    var state = {
        formatDate: createDateFormatter({}),
        tableDateFormat: config.tableDateFormat || "Y-m-d",
        defaults: {
            minTime: undefined,
            maxTime: undefined,
        },
    };
    function findDateTimeLimit(date) {
        if (config.table !== undefined) {
            return config.table[state.formatDate(date, state.tableDateFormat)];
        }
        return config.getTimeLimits && config.getTimeLimits(date);
    }
    return function (fp) {
        return {
            onReady: function () {
                state.formatDate = this.formatDate;
                state.defaults = {
                    minTime: this.config.minTime && state.formatDate(this.config.minTime, "H:i"),
                    maxTime: this.config.maxTime && state.formatDate(this.config.maxTime, "H:i"),
                };
            },
            onChange: function () {
                var latest = this.latestSelectedDateObj;
                var matchingTimeLimit = latest && findDateTimeLimit(latest);
                if (latest && matchingTimeLimit !== undefined) {
                    this.set(matchingTimeLimit);
                    fp.config.minTime.setFullYear(latest.getFullYear());
                    fp.config.maxTime.setFullYear(latest.getFullYear());
                    fp.config.minTime.setMonth(latest.getMonth());
                    fp.config.maxTime.setMonth(latest.getMonth());
                    fp.config.minTime.setDate(latest.getDate());
                    fp.config.maxTime.setDate(latest.getDate());
                    if (compareDates(latest, fp.config.maxTime, false) > 0) {
                        fp.setDate(new Date(latest.getTime()).setHours(fp.config.maxTime.getHours(), fp.config.maxTime.getMinutes(), 0, 0), false);
                    }
                    else if (compareDates(latest, fp.config.minTime, false) < 0)
                        fp.setDate(new Date(latest.getTime()).setHours(fp.config.minTime.getHours(), fp.config.minTime.getMinutes(), 0, 0), false);
                }
                else {
                    var newMinMax = state.defaults || {
                        minTime: undefined,
                        maxTime: undefined,
                    };
                    this.set(newMinMax);
                    if (!latest)
                        return;
                    var _a = fp.config, minTime = _a.minTime, maxTime = _a.maxTime;
                    if (minTime && compareTimes(latest, minTime) < 0) {
                        fp.setDate(new Date(latest.getTime()).setHours(minTime.getHours(), minTime.getMinutes(), minTime.getSeconds()), false);
                    }
                    else if (maxTime && compareTimes(latest, maxTime) > 0) {
                        fp.setDate(new Date(latest.getTime()).setHours(maxTime.getHours(), maxTime.getMinutes(), maxTime.getSeconds()));
                    }
                }
            },
        };
    };
}
export default minMaxTimePlugin;
