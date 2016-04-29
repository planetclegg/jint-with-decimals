﻿using System;
using System.Globalization;
using Jint.Runtime;
using Jint.Runtime.Interop;

namespace Jint.Native.Date
{
    /// <summary>
    /// http://www.ecma-international.org/ecma-262/5.1/#sec-15.9.5
    /// </summary>
    public sealed class DatePrototype : DateInstance
    {
        private DatePrototype(Engine engine)
            : base(engine)
        {
        }

        public static DatePrototype CreatePrototypeObject(Engine engine, DateConstructor dateConstructor)
        {
            var obj = new DatePrototype(engine)
            {
                Prototype = engine.Object.PrototypeObject,
                Extensible = true,
                PrimitiveValue = Money.NaN
            };

            obj.FastAddProperty("constructor", dateConstructor, true, false, true);

            return obj;
        }

        public void Configure()
        {
            FastAddProperty("toString", new ClrFunctionInstance(Engine, ToString, 0), true, false, true);
            FastAddProperty("toDateString", new ClrFunctionInstance(Engine, ToDateString, 0), true, false, true);
            FastAddProperty("toTimeString", new ClrFunctionInstance(Engine, ToTimeString, 0), true, false, true);
            FastAddProperty("toLocaleString", new ClrFunctionInstance(Engine, ToLocaleString, 0), true, false, true);
            FastAddProperty("toLocaleDateString", new ClrFunctionInstance(Engine, ToLocaleDateString, 0), true, false, true);
            FastAddProperty("toLocaleTimeString", new ClrFunctionInstance(Engine, ToLocaleTimeString, 0), true, false, true);
            FastAddProperty("valueOf", new ClrFunctionInstance(Engine, ValueOf, 0), true, false, true);
            FastAddProperty("getTime", new ClrFunctionInstance(Engine, GetTime, 0), true, false, true);
            FastAddProperty("getFullYear", new ClrFunctionInstance(Engine, GetFullYear, 0), true, false, true);
            FastAddProperty("getYear", new ClrFunctionInstance(Engine, GetYear, 0), true, false, true);
            FastAddProperty("getUTCFullYear", new ClrFunctionInstance(Engine, GetUTCFullYear, 0), true, false, true);
            FastAddProperty("getMonth", new ClrFunctionInstance(Engine, GetMonth, 0), true, false, true);
            FastAddProperty("getUTCMonth", new ClrFunctionInstance(Engine, GetUTCMonth, 0), true, false, true);
            FastAddProperty("getDate", new ClrFunctionInstance(Engine, GetDate, 0), true, false, true);
            FastAddProperty("getUTCDate", new ClrFunctionInstance(Engine, GetUTCDate, 0), true, false, true);
            FastAddProperty("getDay", new ClrFunctionInstance(Engine, GetDay, 0), true, false, true);
            FastAddProperty("getUTCDay", new ClrFunctionInstance(Engine, GetUTCDay, 0), true, false, true);
            FastAddProperty("getHours", new ClrFunctionInstance(Engine, GetHours, 0), true, false, true);
            FastAddProperty("getUTCHours", new ClrFunctionInstance(Engine, GetUTCHours, 0), true, false, true);
            FastAddProperty("getMinutes", new ClrFunctionInstance(Engine, GetMinutes, 0), true, false, true);
            FastAddProperty("getUTCMinutes", new ClrFunctionInstance(Engine, GetUTCMinutes, 0), true, false, true);
            FastAddProperty("getSeconds", new ClrFunctionInstance(Engine, GetSeconds, 0), true, false, true);
            FastAddProperty("getUTCSeconds", new ClrFunctionInstance(Engine, GetUTCSeconds, 0), true, false, true);
            FastAddProperty("getMilliseconds", new ClrFunctionInstance(Engine, GetMilliseconds, 0), true, false, true);
            FastAddProperty("getUTCMilliseconds", new ClrFunctionInstance(Engine, GetUTCMilliseconds, 0), true, false, true);
            FastAddProperty("getTimezoneOffset", new ClrFunctionInstance(Engine, GetTimezoneOffset, 0), true, false, true);
            FastAddProperty("setTime", new ClrFunctionInstance(Engine, SetTime, 1), true, false, true);
            FastAddProperty("setMilliseconds", new ClrFunctionInstance(Engine, SetMilliseconds, 1), true, false, true);
            FastAddProperty("setUTCMilliseconds", new ClrFunctionInstance(Engine, SetUTCMilliseconds, 1), true, false, true);
            FastAddProperty("setSeconds", new ClrFunctionInstance(Engine, SetSeconds, 2), true, false, true);
            FastAddProperty("setUTCSeconds", new ClrFunctionInstance(Engine, SetUTCSeconds, 2), true, false, true);
            FastAddProperty("setMinutes", new ClrFunctionInstance(Engine, SetMinutes, 3), true, false, true);
            FastAddProperty("setUTCMinutes", new ClrFunctionInstance(Engine, SetUTCMinutes, 3), true, false, true);
            FastAddProperty("setHours", new ClrFunctionInstance(Engine, SetHours, 4), true, false, true);
            FastAddProperty("setUTCHours", new ClrFunctionInstance(Engine, SetUTCHours, 4), true, false, true);
            FastAddProperty("setDate", new ClrFunctionInstance(Engine, SetDate, 1), true, false, true);
            FastAddProperty("setUTCDate", new ClrFunctionInstance(Engine, SetUTCDate, 1), true, false, true);
            FastAddProperty("setMonth", new ClrFunctionInstance(Engine, SetMonth, 2), true, false, true);
            FastAddProperty("setUTCMonth", new ClrFunctionInstance(Engine, SetUTCMonth, 2), true, false, true);
            FastAddProperty("setFullYear", new ClrFunctionInstance(Engine, SetFullYear, 3), true, false, true);
            FastAddProperty("setYear", new ClrFunctionInstance(Engine, SetYear, 1), true, false, true);
            FastAddProperty("setUTCFullYear", new ClrFunctionInstance(Engine, SetUTCFullYear, 3), true, false, true);
            FastAddProperty("toUTCString", new ClrFunctionInstance(Engine, ToUtcString, 0), true, false, true);
            FastAddProperty("toISOString", new ClrFunctionInstance(Engine, ToISOString, 0), true, false, true);
            FastAddProperty("toJSON", new ClrFunctionInstance(Engine, ToJSON, 1), true, false, true);
        }

        private JsValue ValueOf(JsValue thisObj, JsValue[] arguments)
        {
            return EnsureDateInstance(thisObj).PrimitiveValue;
        }

        /// <summary>
        /// Converts a value to a <see cref="DateInstance"/> or throws a TypeError exception.
        /// c.f., http://www.ecma-international.org/ecma-262/5.1/#sec-15.9.5
        /// </summary>
        private DateInstance EnsureDateInstance(JsValue thisObj)
        {
            return thisObj.TryCast<DateInstance>(value =>
               {
                   throw new JavaScriptException(Engine.TypeError, "Invalid Date");
               });
        }

        public JsValue ToString(JsValue thisObj, JsValue[] arg2)
        {
            return ToLocalTime(EnsureDateInstance(thisObj).ToDateTime()).ToString("ddd MMM dd yyyy HH:mm:ss 'GMT'K", CultureInfo.InvariantCulture);
        }

        private JsValue ToDateString(JsValue thisObj, JsValue[] arguments)
        {
            return ToLocalTime(EnsureDateInstance(thisObj).ToDateTime()).ToString("ddd MMM dd yyyy", CultureInfo.InvariantCulture);
        }

        private JsValue ToTimeString(JsValue thisObj, JsValue[] arguments)
        {
            return ToLocalTime(EnsureDateInstance(thisObj).ToDateTime()).ToString("HH:mm:ss 'GMT'K", CultureInfo.InvariantCulture);
        }

        private JsValue ToLocaleString(JsValue thisObj, JsValue[] arguments)
        {
            return ToLocalTime(EnsureDateInstance(thisObj).ToDateTime()).ToString("F", Engine.Options._Culture);
        }

        private JsValue ToLocaleDateString(JsValue thisObj, JsValue[] arguments)
        {
            return ToLocalTime(EnsureDateInstance(thisObj).ToDateTime()).ToString("D", Engine.Options._Culture);
        }

        private JsValue ToLocaleTimeString(JsValue thisObj, JsValue[] arguments)
        {
            return ToLocalTime(EnsureDateInstance(thisObj).ToDateTime()).ToString("T", Engine.Options._Culture);
        }

        private JsValue GetTime(JsValue thisObj, JsValue[] arguments)
        {
            if (Money.IsNaN(EnsureDateInstance(thisObj).PrimitiveValue))
            {
                return Money.NaN;
            }

            return EnsureDateInstance(thisObj).PrimitiveValue;
        }

        private JsValue GetFullYear(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            if (Money.IsNaN(t))
            {
                return Money.NaN;
            }

            return YearFromTime(LocalTime(t));
        }

        private JsValue GetYear(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            if (Money.IsNaN(t))
            {
                return Money.NaN;
            }

            return YearFromTime(LocalTime(t)) - 1900;
        }

        private JsValue GetUTCFullYear(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            if (Money.IsNaN(t))
            {
                return Money.NaN;
            }

            return YearFromTime(t);
        }

        private JsValue GetMonth(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            if (Money.IsNaN(t))
            {
                return Money.NaN;
            }

            return MonthFromTime(LocalTime(t));
        }

        private JsValue GetUTCMonth(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            if (Money.IsNaN(t))
            {
                return Money.NaN;
            }

            return MonthFromTime(t);
        }

        private JsValue GetDate(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            if (Money.IsNaN(t))
            {
                return Money.NaN;
            }

            return DateFromTime(LocalTime(t));
        }

        private JsValue GetUTCDate(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            if (Money.IsNaN(t))
            {
                return Money.NaN;
            }

            return DateFromTime(t);
        }

        private JsValue GetDay(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            if (Money.IsNaN(t))
            {
                return Money.NaN;
            }

            return WeekDay(LocalTime(t));
        }

        private JsValue GetUTCDay(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            if (Money.IsNaN(t))
            {
                return Money.NaN;
            }

            return WeekDay(t);
        }

        private JsValue GetHours(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            if (Money.IsNaN(t))
            {
                return Money.NaN;
            }

            return HourFromTime(LocalTime(t));
        }

        private JsValue GetUTCHours(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            if (Money.IsNaN(t))
            {
                return Money.NaN;
            }

            return HourFromTime(t);
        }

        private JsValue GetMinutes(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            if (Money.IsNaN(t))
            {
                return Money.NaN;
            }

            return MinFromTime(LocalTime(t));
        }

        private JsValue GetUTCMinutes(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            if (Money.IsNaN(t))
            {
                return Money.NaN;
            }

            return MinFromTime(t);
        }

        private JsValue GetSeconds(JsValue thisObj, JsValue[] arguments)
        {
            var t = thisObj.TryCast<DateInstance>().PrimitiveValue;
            if (Money.IsNaN(t))
            {
                return Money.NaN;
            }

            return SecFromTime(LocalTime(t));
        }

        private JsValue GetUTCSeconds(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            if (Money.IsNaN(t))
            {
                return Money.NaN;
            }

            return SecFromTime(t);
        }

        private JsValue GetMilliseconds(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            if (Money.IsNaN(t))
            {
                return Money.NaN;
            }

            return MsFromTime(LocalTime(t));
        }

        private JsValue GetUTCMilliseconds(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            if (Money.IsNaN(t))
            {
                return Money.NaN;
            }

            return MsFromTime(t);
        }

        private JsValue GetTimezoneOffset(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            if (Money.IsNaN(t))
            {
                return Money.NaN;
            }

            return (t - LocalTime(t))/MsPerMinute;
        }

        private JsValue SetTime(JsValue thisObj, JsValue[] arguments)
        {
            return EnsureDateInstance(thisObj).PrimitiveValue = TimeClip(TypeConverter.ToNumber(arguments.At(0)));
        }

        private JsValue SetMilliseconds(JsValue thisObj, JsValue[] arguments)
        {
            var t = LocalTime(EnsureDateInstance(thisObj).PrimitiveValue);
            var time = MakeTime(HourFromTime(t), MinFromTime(t), SecFromTime(t), TypeConverter.ToNumber(arguments.At(0)));
            var u = TimeClip(Utc(MakeDate(Day(t), time)));
            thisObj.As<DateInstance>().PrimitiveValue = u;
            return u;
        }

        private JsValue SetUTCMilliseconds(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            var time = MakeTime(HourFromTime(t), MinFromTime(t), SecFromTime(t), TypeConverter.ToNumber(arguments.At(0)));
            var u = TimeClip(MakeDate(Day(t), time));
            thisObj.As<DateInstance>().PrimitiveValue = u;
            return u;
        }

        private JsValue SetSeconds(JsValue thisObj, JsValue[] arguments)
        {
            var t = LocalTime(EnsureDateInstance(thisObj).PrimitiveValue);
            var s = TypeConverter.ToNumber(arguments.At(0));
            var milli = arguments.Length <= 1 ? MsFromTime(t) : TypeConverter.ToNumber(arguments.At(1));
            var date = MakeDate(Day(t), MakeTime(HourFromTime(t), MinFromTime(t), s, milli));
            var u = TimeClip(Utc(date));
            thisObj.As<DateInstance>().PrimitiveValue = u;
            return u;
        }

        private JsValue SetUTCSeconds(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            var s = TypeConverter.ToNumber(arguments.At(0));
            var milli = arguments.Length <= 1 ? MsFromTime(t) : TypeConverter.ToNumber(arguments.At(1));
            var date = MakeDate(Day(t), MakeTime(HourFromTime(t), MinFromTime(t), s, milli));
            var u = TimeClip(date);
            thisObj.As<DateInstance>().PrimitiveValue = u;
            return u;
        }

        private JsValue SetMinutes(JsValue thisObj, JsValue[] arguments)
        {
            var t = LocalTime(EnsureDateInstance(thisObj).PrimitiveValue);
            var m = TypeConverter.ToNumber(arguments.At(0));
            var s = arguments.Length <= 1 ? SecFromTime(t) : TypeConverter.ToNumber(arguments.At(1));
            var milli = arguments.Length <= 2 ? MsFromTime(t) : TypeConverter.ToNumber(arguments.At(2));
            var date = MakeDate(Day(t), MakeTime(HourFromTime(t), m, s, milli));
            var u = TimeClip(Utc(date));
            thisObj.As<DateInstance>().PrimitiveValue = u;
            return u;
        }

        private JsValue SetUTCMinutes(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            var m = TypeConverter.ToNumber(arguments.At(0));
            var s = arguments.Length <= 1 ? SecFromTime(t) : TypeConverter.ToNumber(arguments.At(1));
            var milli = arguments.Length <= 2 ? MsFromTime(t) : TypeConverter.ToNumber(arguments.At(2));
            var date = MakeDate(Day(t), MakeTime(HourFromTime(t), m, s, milli));
            var u = TimeClip(date);
            thisObj.As<DateInstance>().PrimitiveValue = u;
            return u;
        }

        private JsValue SetHours(JsValue thisObj, JsValue[] arguments)
        {
            var t = LocalTime(EnsureDateInstance(thisObj).PrimitiveValue);
            var h = TypeConverter.ToNumber(arguments.At(0));
            var m = arguments.Length <= 1 ? MinFromTime(t) : TypeConverter.ToNumber(arguments.At(1));
            var s = arguments.Length <= 2 ? SecFromTime(t) : TypeConverter.ToNumber(arguments.At(2));
            var milli = arguments.Length <= 3 ? MsFromTime(t) : TypeConverter.ToNumber(arguments.At(3));
            var date = MakeDate(Day(t), MakeTime(h, m, s, milli));
            var u = TimeClip(Utc(date));
            thisObj.As<DateInstance>().PrimitiveValue = u;
            return u;
        }

        private JsValue SetUTCHours(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            var h = TypeConverter.ToNumber(arguments.At(0));
            var m = arguments.Length <= 1 ? MinFromTime(t) : TypeConverter.ToNumber(arguments.At(1));
            var s = arguments.Length <= 2 ? SecFromTime(t) : TypeConverter.ToNumber(arguments.At(2));
            var milli = arguments.Length <= 3 ? MsFromTime(t) : TypeConverter.ToNumber(arguments.At(3));
            var date = MakeDate(Day(t), MakeTime(h, m, s, milli));
            var u = TimeClip(date);
            thisObj.As<DateInstance>().PrimitiveValue = u;
            return u;
        }

        private JsValue SetDate(JsValue thisObj, JsValue[] arguments)
        {
            var t = LocalTime(EnsureDateInstance(thisObj).PrimitiveValue);
            var dt = TypeConverter.ToNumber(arguments.At(0));
            var newDate = MakeDate(MakeDay(YearFromTime(t), MonthFromTime(t), dt), TimeWithinDay(t));
            var u = TimeClip(Utc(newDate));
            thisObj.As<DateInstance>().PrimitiveValue = u;
            return u;
        }

        private JsValue SetUTCDate(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            var dt = TypeConverter.ToNumber(arguments.At(0));
            var newDate = MakeDate(MakeDay(YearFromTime(t), MonthFromTime(t), dt), TimeWithinDay(t));
            var u = TimeClip(newDate);
            thisObj.As<DateInstance>().PrimitiveValue = u;
            return u;
        }

        private JsValue SetMonth(JsValue thisObj, JsValue[] arguments)
        {
            var t = LocalTime(EnsureDateInstance(thisObj).PrimitiveValue);
            var m = TypeConverter.ToNumber(arguments.At(0));
            var dt = arguments.Length <= 1 ? DateFromTime(t) : TypeConverter.ToNumber(arguments.At(1));
            var newDate = MakeDate(MakeDay(YearFromTime(t), m, dt), TimeWithinDay(t));
            var u = TimeClip(Utc(newDate));
            thisObj.As<DateInstance>().PrimitiveValue = u;
            return u;
        }

        private JsValue SetUTCMonth(JsValue thisObj, JsValue[] arguments)
        {
            var t = EnsureDateInstance(thisObj).PrimitiveValue;
            var m = TypeConverter.ToNumber(arguments.At(0));
            var dt = arguments.Length <= 1 ? DateFromTime(t) : TypeConverter.ToNumber(arguments.At(1));
            var newDate = MakeDate(MakeDay(YearFromTime(t), m, dt), TimeWithinDay(t));
            var u = TimeClip(newDate);
            thisObj.As<DateInstance>().PrimitiveValue = u;
            return u;
        }

        private JsValue SetFullYear(JsValue thisObj, JsValue[] arguments)
        {
            var thisTime = EnsureDateInstance(thisObj).PrimitiveValue;
            var t = Money.IsNaN(thisTime) ? +0 : LocalTime(thisTime);
            var y = TypeConverter.ToNumber(arguments.At(0));
            var m = arguments.Length <= 1 ? MonthFromTime(t) : TypeConverter.ToNumber(arguments.At(1));
            var dt = arguments.Length <= 2 ? DateFromTime(t) : TypeConverter.ToNumber(arguments.At(2));
            var newDate = MakeDate(MakeDay(y, m, dt), TimeWithinDay(t));
            var u = TimeClip(Utc(newDate));
            thisObj.As<DateInstance>().PrimitiveValue = u;
            return u;
        }

        private JsValue SetYear(JsValue thisObj, JsValue[] arguments)
        {
            var thisTime = EnsureDateInstance(thisObj).PrimitiveValue;
            var t = Money.IsNaN(thisTime) ? +0 : LocalTime(thisTime);
            var y = TypeConverter.ToNumber(arguments.At(0));
            if (Money.IsNaN(y))
            {
                EnsureDateInstance(thisObj).PrimitiveValue = Money.NaN;
                return Money.NaN;
            }

            var fy = TypeConverter.ToInteger(y);
            if (y >= 0 && y <= 99)
            {
                fy = fy + 1900;
            }

            var newDate = MakeDay(fy, MonthFromTime(t), DateFromTime(t));
            var u = Utc(MakeDate(newDate, TimeWithinDay(t)));
            EnsureDateInstance(thisObj).PrimitiveValue = TimeClip(u);
            return u;
        }

        private JsValue SetUTCFullYear(JsValue thisObj, JsValue[] arguments)
        {
            var thisTime = EnsureDateInstance(thisObj).PrimitiveValue;
            var t = Money.IsNaN(thisTime) ? +0 : thisTime;
            var y = TypeConverter.ToNumber(arguments.At(0));
            var m = arguments.Length <= 1 ? MonthFromTime(t) : TypeConverter.ToNumber(arguments.At(1));
            var dt = arguments.Length <= 2 ? DateFromTime(t) : TypeConverter.ToNumber(arguments.At(2));
            var newDate = MakeDate(MakeDay(y, m, dt), TimeWithinDay(t));
            var u = TimeClip(newDate);
            thisObj.As<DateInstance>().PrimitiveValue = u;
            return u;
        }

        private JsValue ToUtcString(JsValue thisObj, JsValue[] arguments)
        {
            return thisObj.TryCast<DateInstance>(x =>
            {
                throw new JavaScriptException(Engine.TypeError);
            } )
            .ToDateTime().ToUniversalTime().ToString("ddd MMM dd yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture);
        }

        private JsValue ToISOString(JsValue thisObj, JsValue[] arguments)
        {
            var t = thisObj.TryCast<DateInstance>(x =>
            {
                throw new JavaScriptException(Engine.TypeError);
            }).PrimitiveValue;

            if (Money.IsInfinity(t) ||  Money.IsNaN(t))
            {
                throw new JavaScriptException(Engine.RangeError);
            }
            Money h = HourFromTime(t);
            Money m = MinFromTime(t);
            Money s = SecFromTime(t);
            Money ms = MsFromTime(t);
            if (h < 0) { h += HoursPerDay; }
            if (m < 0) { m += MinutesPerHour; }
            if (s < 0) { s += SecondsPerMinute; }
            if (ms < 0) { ms += MsPerSecond; }
            return string.Format("{0:0000}-{1:00}-{2:00}T{3:00}:{4:00}:{5:00}.{6:000}Z",
                YearFromTime(t),
                MonthFromTime(t)+1,
                DateFromTime(t),
                h,
                m,
                s,
                ms);
        }

        private JsValue ToJSON(JsValue thisObj, JsValue[] arguments)
        {
            var o = TypeConverter.ToObject(Engine, thisObj);
            var tv = TypeConverter.ToPrimitive(o, Types.Number);
            if (tv.IsNumber() && Money.IsInfinity(tv.AsNumber()))
            {
                return JsValue.Null;
            }

            var toIso = o.Get("toISOString");
            if (!toIso.Is<ICallable>())
            {
                throw new JavaScriptException(Engine.TypeError);
            }

            return toIso.TryCast<ICallable>().Call(o, Arguments.Empty);
        }

        public const decimal HoursPerDay = 24;
		public const decimal MinutesPerHour = 60;
		public const decimal SecondsPerMinute = 60;
		public const decimal MsPerSecond = 1000;
		public const decimal MsPerMinute = 60000;
		public const decimal MsPerHour = 3600000;
		public const decimal MsPerDay = 86400000;

        /// <summary>
        /// 15.9.1.2
        /// </summary>
        public static Money Day(Money t)
        {
            return Money.Floor(t / MsPerDay);
        }

        /// <summary>
        /// 15.9.1.2
        /// </summary>
        public static Money TimeWithinDay(Money t)
        {
            return t % MsPerDay;
        }

        /// <summary>
        /// The number of days in a year
        /// </summary>
        public static Money DaysInYear(Money y)
        {
            if (!(y%4).Equals(0))
            {
                return 365;
            }

            if ((y%4).Equals(0) && !(y%100).Equals(0))
            {
                return 366;
            }

            if ((y%100).Equals(0) && !(y%400).Equals(0))
            {
                return 365;
            }

            if ((y%400).Equals(0))
            {
                return 366;
            }

            return 365;
        }

        /// <summary>
        /// The day number of the first day of the year.
        /// </summary>
        public static Money DayFromYear(Money y)
        {
            return 365*(y - 1970)
                + Money.Floor((y - 1969)/4)
                - Money.Floor((y - 1901)/100)
                + Money.Floor((y - 1601)/400);
        }

        /// <summary>
        /// The time value of the start of the year
        /// </summary>
        public static Money TimeFromYear(Money y)
        {
            return MsPerDay*DayFromYear(y);
        }

        /// <summary>
        /// The year of a time value.
        /// </summary>
        public static Money YearFromTime(Money t)
        {
            if (!AreFinite(t))
            {
                return Money.NaN;
            }

            // FIXME: JEC hack to fix overflows in date calculations.. dubious
            Money upper = Money.MaxValue / (MsPerDay * 365 * 2);
            Money lower = Money.MinValue / (MsPerDay * 365 * 2);
            while (upper > lower + 1)
            {
                var current = Money.Floor((upper + lower) / 2);

                var tfy = TimeFromYear(current);

                if (tfy <= t)
                {
                    lower = current;
                }
                else
                {
                    upper = current;
                }
            }

            return lower;
        }

        /// <summary>
        /// <value>true</value> if the time is within a leap year, <value>false</value> otherwise
        /// </summary>
        public static Money InLeapYear(Money t)
        {
            var daysInYear = DaysInYear(YearFromTime(t));

            if (daysInYear.Equals(365))
            {
                return 0;
            }

            if (daysInYear.Equals(366))
            {
                return 1;
            }

            throw new ArgumentException();
        }

        /// <summary>
        /// The month number of a time value.
        /// </summary>
        public static Money MonthFromTime(Money t)
        {
            var dayWithinYear = DayWithinYear(t);
            var inLeapYear = InLeapYear(t);

            if (dayWithinYear < 31)
            {
                return 0;
            }

            if (dayWithinYear < 59 + inLeapYear)
            {
                return 1;
            }

            if (dayWithinYear < 90 + inLeapYear)
            {
                return 2;
            }

            if (dayWithinYear < 120 + inLeapYear)
            {
                return 3;
            }

            if (dayWithinYear < 151 + inLeapYear)
            {
                return 4;
            }

            if (dayWithinYear < 181 + inLeapYear)
            {
                return 5;
            }

            if (dayWithinYear < 212 + inLeapYear)
            {
                return 6;
            }

            if (dayWithinYear < 243 + inLeapYear)
            {
                return 7;
            }

            if (dayWithinYear < 273 + inLeapYear)
            {
                return 8;
            }

            if (dayWithinYear < 304 + inLeapYear)
            {
                return 9;
            }

            if (dayWithinYear < 334 + inLeapYear)
            {
                return 10;
            }

            if (dayWithinYear < 365 + inLeapYear)
            {
                return 11;
            }

            throw new InvalidOperationException();
        }

        public static Money DayWithinYear(Money t)
        {
            return Day(t) - DayFromYear(YearFromTime(t));
        }

        public static Money DateFromTime(Money t)
        {
            var monthFromTime = MonthFromTime(t);
            var dayWithinYear = DayWithinYear(t);

            if (monthFromTime.Equals(0))
            {
                return dayWithinYear + 1;
            }

            if (monthFromTime.Equals(1))
            {
                return dayWithinYear - 30;
            }

            if (monthFromTime.Equals(2))
            {
                return dayWithinYear - 58 - InLeapYear(t);
            }

            if (monthFromTime.Equals(3))
            {
                return dayWithinYear - 89 - InLeapYear(t);
            }

            if (monthFromTime.Equals(4))
            {
                return dayWithinYear - 119 - InLeapYear(t);
            }

            if (monthFromTime.Equals(5))
            {
                return dayWithinYear - 150 - InLeapYear(t);
            }

            if (monthFromTime.Equals(6))
            {
                return dayWithinYear - 180 - InLeapYear(t);
            }

            if (monthFromTime.Equals(7))
            {
                return dayWithinYear - 211 - InLeapYear(t);
            }

            if (monthFromTime.Equals(8))
            {
                return dayWithinYear - 242 - InLeapYear(t);
            }

            if (monthFromTime.Equals(9))
            {
                return dayWithinYear - 272 - InLeapYear(t);
            }

            if (monthFromTime.Equals(10))
            {
                return dayWithinYear - 303 - InLeapYear(t);
            }

            if (monthFromTime.Equals(11))
            {
                return dayWithinYear - 333 - InLeapYear(t);
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// The weekday for a particular time value.
        /// </summary>
        public static Money WeekDay(Money t)
        {
            return (Day(t) + 4)%7;
        }

        public Money LocalTza
        {
            get
            {
				return (decimal)Engine.Options._LocalTimeZone.BaseUtcOffset.TotalMilliseconds;
            }
        }

        public Money DaylightSavingTa(Money t)
        {
            var timeInYear = t - TimeFromYear(YearFromTime(t));

            if (Money.IsInfinity(timeInYear) || Money.IsNaN(timeInYear))
            {
                return 0;
            }

            var year = YearFromTime(t);
            if (year < 9999 && year > -9999)
            {
                // in DateTimeOffset range so we can use it
            }
            else
            {
                // use similar leap-ed year
                var isLeapYear = InLeapYear(t).Equals(1);
                year = isLeapYear ? 2000 : 1999;
            }

			var dateTime = new DateTime((int)year, 1, 1).AddMilliseconds(timeInYear.ToDouble());

            return Engine.Options._LocalTimeZone.IsDaylightSavingTime(dateTime) ? MsPerHour : 0;
        }

        public DateTimeOffset ToLocalTime(DateTime t)	
        {
            switch (t.Kind)
            {
                case DateTimeKind.Local:
                    return new DateTimeOffset(TimeZoneInfo.ConvertTime(t.ToUniversalTime(), Engine.Options._LocalTimeZone), Engine.Options._LocalTimeZone.GetUtcOffset(t));
                case DateTimeKind.Utc:
                    return new DateTimeOffset(TimeZoneInfo.ConvertTime(t, Engine.Options._LocalTimeZone), Engine.Options._LocalTimeZone.GetUtcOffset(t));
                default:
                    return t;
            }
        }

        public Money LocalTime(Money t)
        {
            return t + LocalTza + DaylightSavingTa(t);
        }

        public Money Utc(Money t)
        {
            return t - LocalTza - DaylightSavingTa(t - LocalTza);
        }

        public static Money HourFromTime(Money t)
        {
            return Money.Floor(t / MsPerHour) % HoursPerDay;
        }

        public static Money MinFromTime(Money t)
        {
            return Money.Floor(t / MsPerMinute) % MinutesPerHour;
        }

        public static Money SecFromTime(Money t)
        {
            return Money.Floor(t / MsPerSecond) % SecondsPerMinute;
        }

        public static Money MsFromTime(Money t)
        {
            return t % MsPerSecond;
        }

        public static Money DayFromMonth(Money year, Money month)
        {
            Money day = month * 30;

            if (month >= 7)
            {
                day += month/2 - 1;
            }
            else if (month >= 2)
            {
                day += (month - 1)/2 - 1;
            }
            else
            {
                day += month;
            }

            if (month >= 2 && InLeapYear(year).Equals(1))
            {
                day++;
            }

            return day;
        }


        public static Money DaysInMonth(Money month, Money leap)
        {
            month = month%12;

            switch ((long) month)
            {
                case 0:
                case 2:
                case 4:
                case 6:
                case 7:
                case 9:
                case 11:
                    return 31;
                case 3:
                case 5:
                case 8:
                case 10:
                    return 30;
                case 1:
                    return 28 + leap;
                default:
                    throw new ArgumentOutOfRangeException("month");

            }
        }

        public static Money MakeTime(Money hour, Money min, Money sec, Money ms)
        {
            if (!AreFinite(hour, min, sec, ms))
            {
                return Money.NaN;
            }

            var h = (long) hour;
            var m = (long) min;
            var s = (long) sec;
            var milli = (long) ms;
            var t = h*MsPerHour + m*MsPerMinute + s*MsPerSecond + milli;

            return t;
        }

        public static Money MakeDay(Money year, Money month, Money date)
        {
            if (!AreFinite(year, month, date))
            {
                return Money.NaN;
            }

            year = TypeConverter.ToInteger(year);
            month = TypeConverter.ToInteger(month);
            date = TypeConverter.ToInteger(date);

            var sign = (year < 1970) ? -1 : 1;
            Money t = (year < 1970) ? 1 : 0;
            int y;

            if (sign == -1)
            {
                for (y = 1969; y >= year; y += sign)
                {
                    t += sign * DaysInYear(y) * MsPerDay;
                }
            }
            else
            {
                for (y = 1970; y < year; y += sign)
                {
                    t += sign * DaysInYear(y) * MsPerDay;
                }
            }

            for (var m = 0; m < month; m++)
            {
                t += DaysInMonth(m, InLeapYear(t)) * MsPerDay;
            }

            return Day(t) + date - 1;
        }

        public static Money MakeDate(Money day, Money time)
        {
            if (!AreFinite(day, time))
            {
                return Money.NaN;
            }

            return day * MsPerDay + time;
        }

        public static Money TimeClip(Money time)
        {
            if (!AreFinite(time))
            {
                return Money.NaN;
            }

            if (Money.Abs(time) > 8640000000000000)
            {
                return Money.NaN;
            }

            return (long) time + 0;
        }

        private static bool AreFinite(params Money[] values)
        {
            for (int index = 0; index < values.Length; index++)
            {
                var value = values[index];
                if (Money.IsNaN(value) || Money.IsInfinity(value))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
