var fp = typeof window !== "undefined" && window.flatpickr !== undefined
    ? window.flatpickr
    : {
        l10ns: {},
    };
export var Bulgarian = {
    weekdays: {
        shorthand: ["Нд", "Пн", "Вт", "Ср", "Чт", "Пт", "Сб"],
        longhand: [
            "Неделя",
            "Понеделник",
            "Вторник",
            "Сряда",
            "Четвъртък",
            "Петък",
            "Събота",
        ],
    },
    months: {
        shorthand: [
            "Яну",
            "Фев",
            "Март",
            "Апр",
            "Май",
            "Юни",
            "Юли",
            "Авг",
            "Сеп",
            "Окт",
            "Ное",
            "Дек",
        ],
        longhand: [
            "Януари",
            "Февруари",
            "Март",
            "Април",
            "Май",
            "Юни",
            "Юли",
            "Август",
            "Септември",
            "Октомври",
            "Ноември",
            "Декември",
        ],
    },
};
fp.l10ns.bg = Bulgarian;
export default fp.l10ns;
