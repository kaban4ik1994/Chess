﻿/*! SmartAdmin - v1.4.1 - 2014-06-20 */ ! function (a) {
    "undefined" == typeof a.fn.each2 && a.extend(a.fn, {
        each2: function (b) {
            for (var c = a([0]), d = -1, e = this.length; ++d < e && (c.context = c[0] = this[d]) && b.call(c[0], d, c) !== !1;);
            return this
        }
    })
}(jQuery),
function (a, b) {
    "use strict";

    function c(b) {
        var c = a(document.createTextNode(""));
        b.before(c), c.before(b), c.remove()
    }

    function d(a) {
        function b(a) {
            return O[a] || a
        }
        return a.replace(/[^\u0000-\u007E]/g, b)
    }

    function e(a, b) {
        for (var c = 0, d = b.length; d > c; c += 1)
            if (g(a, b[c])) return c;
        return -1
    }

    function f() {
        var b = a(N);
        b.appendTo("body");
        var c = {
            width: b.width() - b[0].clientWidth,
            height: b.height() - b[0].clientHeight
        };
        return b.remove(), c
    }

    function g(a, c) {
        return a === c ? !0 : a === b || c === b ? !1 : null === a || null === c ? !1 : a.constructor === String ? a + "" == c + "" : c.constructor === String ? c + "" == a + "" : !1
    }

    function h(b, c) {
        var d, e, f;
        if (null === b || b.length < 1) return [];
        for (d = b.split(c), e = 0, f = d.length; f > e; e += 1) d[e] = a.trim(d[e]);
        return d
    }

    function i(a) {
        return a.outerWidth(!1) - a.width()
    }

    function j(c) {
        var d = "keyup-change-value";
        c.on("keydown", function () {
            a.data(c, d) === b && a.data(c, d, c.val())
        }), c.on("keyup", function () {
            var e = a.data(c, d);
            e !== b && c.val() !== e && (a.removeData(c, d), c.trigger("keyup-change"))
        })
    }

    function k(c) {
        c.on("mousemove", function (c) {
            var d = M;
            (d === b || d.x !== c.pageX || d.y !== c.pageY) && a(c.target).trigger("mousemove-filtered", c)
        })
    }

    function l(a, c, d) {
        d = d || b;
        var e;
        return function () {
            var b = arguments;
            window.clearTimeout(e), e = window.setTimeout(function () {
                c.apply(d, b)
            }, a)
        }
    }

    function m(a, b) {
        var c = l(a, function (a) {
            b.trigger("scroll-debounced", a)
        });
        b.on("scroll", function (a) {
            e(a.target, b.get()) >= 0 && c(a)
        })
    }

    function n(a) {
        a[0] !== document.activeElement && window.setTimeout(function () {
            var b, c = a[0],
                d = a.val().length;
            a.focus();
            var e = c.offsetWidth > 0 || c.offsetHeight > 0;
            e && c === document.activeElement && (c.setSelectionRange ? c.setSelectionRange(d, d) : c.createTextRange && (b = c.createTextRange(), b.collapse(!1), b.select()))
        }, 0)
    }

    function o(b) {
        b = a(b)[0];
        var c = 0,
            d = 0;
        if ("selectionStart" in b) c = b.selectionStart, d = b.selectionEnd - c;
        else if ("selection" in document) {
            b.focus();
            var e = document.selection.createRange();
            d = document.selection.createRange().text.length, e.moveStart("character", -b.value.length), c = e.text.length - d
        }
        return {
            offset: c,
            length: d
        }
    }

    function p(a) {
        a.preventDefault(), a.stopPropagation()
    }

    function q(a) {
        a.preventDefault(), a.stopImmediatePropagation()
    }

    function r(b) {
        if (!J) {
            var c = b[0].currentStyle || window.getComputedStyle(b[0], null);
            J = a(document.createElement("div")).css({
                position: "absolute",
                left: "-10000px",
                top: "-10000px",
                display: "none",
                fontSize: c.fontSize,
                fontFamily: c.fontFamily,
                fontStyle: c.fontStyle,
                fontWeight: c.fontWeight,
                letterSpacing: c.letterSpacing,
                textTransform: c.textTransform,
                whiteSpace: "nowrap"
            }), J.attr("class", "select2-sizer"), a("body").append(J)
        }
        return J.text(b.val()), J.width()
    }

    function s(b, c, d) {
        var e, f, g = [];
        e = b.attr("class"), e && (e = "" + e, a(e.split(" ")).each2(function () {
            0 === this.indexOf("select2-") && g.push(this)
        })), e = c.attr("class"), e && (e = "" + e, a(e.split(" ")).each2(function () {
            0 !== this.indexOf("select2-") && (f = d(this), f && g.push(f))
        })), b.attr("class", g.join(" "))
    }

    function t(a, b, c, e) {
        var f = d(a.toUpperCase()).indexOf(d(b.toUpperCase())),
            g = b.length;
        return 0 > f ? void c.push(e(a)) : (c.push(e(a.substring(0, f))), c.push("<span class='select2-match'>"), c.push(e(a.substring(f, f + g))), c.push("</span>"), void c.push(e(a.substring(f + g, a.length))))
    }

    function u(a) {
        var b = {
            "\\": "&#92;",
            "&": "&amp;",
            "<": "&lt;",
            ">": "&gt;",
            '"': "&quot;",
            "'": "&#39;",
            "/": "&#47;"
        };
        return String(a).replace(/[&<>"'\/\\]/g, function (a) {
            return b[a]
        })
    }

    function v(c) {
        var d, e = null,
            f = c.quietMillis || 100,
            g = c.url,
            h = this;
        return function (i) {
            window.clearTimeout(d), d = window.setTimeout(function () {
                var d = c.data,
                    f = g,
                    j = c.transport || a.fn.select2.ajaxDefaults.transport,
                    k = {
                        type: c.type || "GET",
                        cache: c.cache || !1,
                        jsonpCallback: c.jsonpCallback || b,
                        dataType: c.dataType || "json"
                    },
                    l = a.extend({}, a.fn.select2.ajaxDefaults.params, k);
                d = d ? d.call(h, i.term, i.page, i.context) : null, f = "function" == typeof f ? f.call(h, i.term, i.page, i.context) : f, e && "function" == typeof e.abort && e.abort(), c.params && (a.isFunction(c.params) ? a.extend(l, c.params.call(h)) : a.extend(l, c.params)), a.extend(l, {
                    url: f,
                    dataType: c.dataType,
                    data: d,
                    success: function (a) {
                        var b = c.results(a, i.page);
                        i.callback(b)
                    }
                }), e = j.call(h, l)
            }, f)
        }
    }

    function w(b) {
        var c, d, e = b,
            f = function (a) {
                return "" + a.text
            };
        a.isArray(e) && (d = e, e = {
            results: d
        }), a.isFunction(e) === !1 && (d = e, e = function () {
            return d
        });
        var g = e();
        return g.text && (f = g.text, a.isFunction(f) || (c = g.text, f = function (a) {
            return a[c]
        })),
            function (b) {
                var c, d = b.term,
                    g = {
                        results: []
                    };
                return "" === d ? void b.callback(e()) : (c = function (e, g) {
                    var h, i;
                    if (e = e[0], e.children) {
                        h = {};
                        for (i in e) e.hasOwnProperty(i) && (h[i] = e[i]);
                        h.children = [], a(e.children).each2(function (a, b) {
                            c(b, h.children)
                        }), (h.children.length || b.matcher(d, f(h), e)) && g.push(h)
                    } else b.matcher(d, f(e), e) && g.push(e)
                }, a(e().results).each2(function (a, b) {
                    c(b, g.results)
                }), void b.callback(g))
            }
    }

    function x(c) {
        var d = a.isFunction(c);
        return function (e) {
            var f = e.term,
                g = {
                    results: []
                },
                h = d ? c(e) : c;
            a.isArray(h) && (a(h).each(function () {
                var a = this.text !== b,
                    c = a ? this.text : this;
                ("" === f || e.matcher(f, c)) && g.results.push(a ? this : {
                    id: this,
                    text: this
                })
            }), e.callback(g))
        }
    }

    function y(b, c) {
        if (a.isFunction(b)) return !0;
        if (!b) return !1;
        if ("string" == typeof b) return !0;
        throw new Error(c + " must be a string, function, or falsy value")
    }

    function z(b) {
        if (a.isFunction(b)) {
            var c = Array.prototype.slice.call(arguments, 1);
            return b.apply(null, c)
        }
        return b
    }

    function A(b) {
        var c = 0;
        return a.each(b, function (a, b) {
            b.children ? c += A(b.children) : c++
        }), c
    }

    function B(a, c, d, e) {
        var f, h, i, j, k, l = a,
            m = !1;
        if (!e.createSearchChoice || !e.tokenSeparators || e.tokenSeparators.length < 1) return b;
        for (; ;) {
            for (h = -1, i = 0, j = e.tokenSeparators.length; j > i && (k = e.tokenSeparators[i], h = a.indexOf(k), !(h >= 0)) ; i++);
            if (0 > h) break;
            if (f = a.substring(0, h), a = a.substring(h + k.length), f.length > 0 && (f = e.createSearchChoice.call(this, f, c), f !== b && null !== f && e.id(f) !== b && null !== e.id(f))) {
                for (m = !1, i = 0, j = c.length; j > i; i++)
                    if (g(e.id(f), e.id(c[i]))) {
                        m = !0;
                        break
                    }
                m || d(f)
            }
        }
        return l !== a ? a : void 0
    }

    function C() {
        var a = this;
        Array.prototype.forEach.call(arguments, function (b) {
            a[b].remove(), a[b] = null
        })
    }

    function D(b, c) {
        var d = function () { };
        return d.prototype = new b, d.prototype.constructor = d, d.prototype.parent = b.prototype, d.prototype = a.extend(d.prototype, c), d
    }
    if (window.Select2 === b) {
        var E, F, G, H, I, J, K, L, M = {
            x: 0,
            y: 0
        },
            E = {
                TAB: 9,
                ENTER: 13,
                ESC: 27,
                SPACE: 32,
                LEFT: 37,
                UP: 38,
                RIGHT: 39,
                DOWN: 40,
                SHIFT: 16,
                CTRL: 17,
                ALT: 18,
                PAGE_UP: 33,
                PAGE_DOWN: 34,
                HOME: 36,
                END: 35,
                BACKSPACE: 8,
                DELETE: 46,
                isArrow: function (a) {
                    switch (a = a.which ? a.which : a) {
                        case E.LEFT:
                        case E.RIGHT:
                        case E.UP:
                        case E.DOWN:
                            return !0
                    }
                    return !1
                },
                isControl: function (a) {
                    var b = a.which;
                    switch (b) {
                        case E.SHIFT:
                        case E.CTRL:
                        case E.ALT:
                            return !0
                    }
                    return a.metaKey ? !0 : !1
                },
                isFunctionKey: function (a) {
                    return a = a.which ? a.which : a, a >= 112 && 123 >= a
                }
            },
            N = "<div class='select2-measure-scrollbar'></div>",
            O = {
                "Ⓐ": "A",
                Ａ: "A",
                À: "A",
                Á: "A",
                Â: "A",
                Ầ: "A",
                Ấ: "A",
                Ẫ: "A",
                Ẩ: "A",
                Ã: "A",
                Ā: "A",
                Ă: "A",
                Ằ: "A",
                Ắ: "A",
                Ẵ: "A",
                Ẳ: "A",
                Ȧ: "A",
                Ǡ: "A",
                Ä: "A",
                Ǟ: "A",
                Ả: "A",
                Å: "A",
                Ǻ: "A",
                Ǎ: "A",
                Ȁ: "A",
                Ȃ: "A",
                Ạ: "A",
                Ậ: "A",
                Ặ: "A",
                Ḁ: "A",
                Ą: "A",
                Ⱥ: "A",
                Ɐ: "A",
                Ꜳ: "AA",
                Æ: "AE",
                Ǽ: "AE",
                Ǣ: "AE",
                Ꜵ: "AO",
                Ꜷ: "AU",
                Ꜹ: "AV",
                Ꜻ: "AV",
                Ꜽ: "AY",
                "Ⓑ": "B",
                Ｂ: "B",
                Ḃ: "B",
                Ḅ: "B",
                Ḇ: "B",
                Ƀ: "B",
                Ƃ: "B",
                Ɓ: "B",
                "Ⓒ": "C",
                Ｃ: "C",
                Ć: "C",
                Ĉ: "C",
                Ċ: "C",
                Č: "C",
                Ç: "C",
                Ḉ: "C",
                Ƈ: "C",
                Ȼ: "C",
                Ꜿ: "C",
                "Ⓓ": "D",
                Ｄ: "D",
                Ḋ: "D",
                Ď: "D",
                Ḍ: "D",
                Ḑ: "D",
                Ḓ: "D",
                Ḏ: "D",
                Đ: "D",
                Ƌ: "D",
                Ɗ: "D",
                Ɖ: "D",
                Ꝺ: "D",
                Ǳ: "DZ",
                Ǆ: "DZ",
                ǲ: "Dz",
                ǅ: "Dz",
                "Ⓔ": "E",
                Ｅ: "E",
                È: "E",
                É: "E",
                Ê: "E",
                Ề: "E",
                Ế: "E",
                Ễ: "E",
                Ể: "E",
                Ẽ: "E",
                Ē: "E",
                Ḕ: "E",
                Ḗ: "E",
                Ĕ: "E",
                Ė: "E",
                Ë: "E",
                Ẻ: "E",
                Ě: "E",
                Ȅ: "E",
                Ȇ: "E",
                Ẹ: "E",
                Ệ: "E",
                Ȩ: "E",
                Ḝ: "E",
                Ę: "E",
                Ḙ: "E",
                Ḛ: "E",
                Ɛ: "E",
                Ǝ: "E",
                "Ⓕ": "F",
                Ｆ: "F",
                Ḟ: "F",
                Ƒ: "F",
                Ꝼ: "F",
                "Ⓖ": "G",
                Ｇ: "G",
                Ǵ: "G",
                Ĝ: "G",
                Ḡ: "G",
                Ğ: "G",
                Ġ: "G",
                Ǧ: "G",
                Ģ: "G",
                Ǥ: "G",
                Ɠ: "G",
                "Ꞡ": "G",
                Ᵹ: "G",
                Ꝿ: "G",
                "Ⓗ": "H",
                Ｈ: "H",
                Ĥ: "H",
                Ḣ: "H",
                Ḧ: "H",
                Ȟ: "H",
                Ḥ: "H",
                Ḩ: "H",
                Ḫ: "H",
                Ħ: "H",
                Ⱨ: "H",
                Ⱶ: "H",
                "Ɥ": "H",
                "Ⓘ": "I",
                Ｉ: "I",
                Ì: "I",
                Í: "I",
                Î: "I",
                Ĩ: "I",
                Ī: "I",
                Ĭ: "I",
                İ: "I",
                Ï: "I",
                Ḯ: "I",
                Ỉ: "I",
                Ǐ: "I",
                Ȉ: "I",
                Ȋ: "I",
                Ị: "I",
                Į: "I",
                Ḭ: "I",
                Ɨ: "I",
                "Ⓙ": "J",
                Ｊ: "J",
                Ĵ: "J",
                Ɉ: "J",
                "Ⓚ": "K",
                Ｋ: "K",
                Ḱ: "K",
                Ǩ: "K",
                Ḳ: "K",
                Ķ: "K",
                Ḵ: "K",
                Ƙ: "K",
                Ⱪ: "K",
                Ꝁ: "K",
                Ꝃ: "K",
                Ꝅ: "K",
                "Ꞣ": "K",
                "Ⓛ": "L",
                Ｌ: "L",
                Ŀ: "L",
                Ĺ: "L",
                Ľ: "L",
                Ḷ: "L",
                Ḹ: "L",
                Ļ: "L",
                Ḽ: "L",
                Ḻ: "L",
                Ł: "L",
                Ƚ: "L",
                Ɫ: "L",
                Ⱡ: "L",
                Ꝉ: "L",
                Ꝇ: "L",
                Ꞁ: "L",
                Ǉ: "LJ",
                ǈ: "Lj",
                "Ⓜ": "M",
                Ｍ: "M",
                Ḿ: "M",
                Ṁ: "M",
                Ṃ: "M",
                Ɱ: "M",
                Ɯ: "M",
                "Ⓝ": "N",
                Ｎ: "N",
                Ǹ: "N",
                Ń: "N",
                Ñ: "N",
                Ṅ: "N",
                Ň: "N",
                Ṇ: "N",
                Ņ: "N",
                Ṋ: "N",
                Ṉ: "N",
                Ƞ: "N",
                Ɲ: "N",
                "Ꞑ": "N",
                "Ꞥ": "N",
                Ǌ: "NJ",
                ǋ: "Nj",
                "Ⓞ": "O",
                Ｏ: "O",
                Ò: "O",
                Ó: "O",
                Ô: "O",
                Ồ: "O",
                Ố: "O",
                Ỗ: "O",
                Ổ: "O",
                Õ: "O",
                Ṍ: "O",
                Ȭ: "O",
                Ṏ: "O",
                Ō: "O",
                Ṑ: "O",
                Ṓ: "O",
                Ŏ: "O",
                Ȯ: "O",
                Ȱ: "O",
                Ö: "O",
                Ȫ: "O",
                Ỏ: "O",
                Ő: "O",
                Ǒ: "O",
                Ȍ: "O",
                Ȏ: "O",
                Ơ: "O",
                Ờ: "O",
                Ớ: "O",
                Ỡ: "O",
                Ở: "O",
                Ợ: "O",
                Ọ: "O",
                Ộ: "O",
                Ǫ: "O",
                Ǭ: "O",
                Ø: "O",
                Ǿ: "O",
                Ɔ: "O",
                Ɵ: "O",
                Ꝋ: "O",
                Ꝍ: "O",
                Ƣ: "OI",
                Ꝏ: "OO",
                Ȣ: "OU",
                "Ⓟ": "P",
                Ｐ: "P",
                Ṕ: "P",
                Ṗ: "P",
                Ƥ: "P",
                Ᵽ: "P",
                Ꝑ: "P",
                Ꝓ: "P",
                Ꝕ: "P",
                "Ⓠ": "Q",
                Ｑ: "Q",
                Ꝗ: "Q",
                Ꝙ: "Q",
                Ɋ: "Q",
                "Ⓡ": "R",
                Ｒ: "R",
                Ŕ: "R",
                Ṙ: "R",
                Ř: "R",
                Ȑ: "R",
                Ȓ: "R",
                Ṛ: "R",
                Ṝ: "R",
                Ŗ: "R",
                Ṟ: "R",
                Ɍ: "R",
                Ɽ: "R",
                Ꝛ: "R",
                "Ꞧ": "R",
                Ꞃ: "R",
                "Ⓢ": "S",
                Ｓ: "S",
                ẞ: "S",
                Ś: "S",
                Ṥ: "S",
                Ŝ: "S",
                Ṡ: "S",
                Š: "S",
                Ṧ: "S",
                Ṣ: "S",
                Ṩ: "S",
                Ș: "S",
                Ş: "S",
                "Ȿ": "S",
                "Ꞩ": "S",
                Ꞅ: "S",
                "Ⓣ": "T",
                Ｔ: "T",
                Ṫ: "T",
                Ť: "T",
                Ṭ: "T",
                Ț: "T",
                Ţ: "T",
                Ṱ: "T",
                Ṯ: "T",
                Ŧ: "T",
                Ƭ: "T",
                Ʈ: "T",
                Ⱦ: "T",
                Ꞇ: "T",
                Ꜩ: "TZ",
                "Ⓤ": "U",
                Ｕ: "U",
                Ù: "U",
                Ú: "U",
                Û: "U",
                Ũ: "U",
                Ṹ: "U",
                Ū: "U",
                Ṻ: "U",
                Ŭ: "U",
                Ü: "U",
                Ǜ: "U",
                Ǘ: "U",
                Ǖ: "U",
                Ǚ: "U",
                Ủ: "U",
                Ů: "U",
                Ű: "U",
                Ǔ: "U",
                Ȕ: "U",
                Ȗ: "U",
                Ư: "U",
                Ừ: "U",
                Ứ: "U",
                Ữ: "U",
                Ử: "U",
                Ự: "U",
                Ụ: "U",
                Ṳ: "U",
                Ų: "U",
                Ṷ: "U",
                Ṵ: "U",
                Ʉ: "U",
                "Ⓥ": "V",
                Ｖ: "V",
                Ṽ: "V",
                Ṿ: "V",
                Ʋ: "V",
                Ꝟ: "V",
                Ʌ: "V",
                Ꝡ: "VY",
                "Ⓦ": "W",
                Ｗ: "W",
                Ẁ: "W",
                Ẃ: "W",
                Ŵ: "W",
                Ẇ: "W",
                Ẅ: "W",
                Ẉ: "W",
                Ⱳ: "W",
                "Ⓧ": "X",
                Ｘ: "X",
                Ẋ: "X",
                Ẍ: "X",
                "Ⓨ": "Y",
                Ｙ: "Y",
                Ỳ: "Y",
                Ý: "Y",
                Ŷ: "Y",
                Ỹ: "Y",
                Ȳ: "Y",
                Ẏ: "Y",
                Ÿ: "Y",
                Ỷ: "Y",
                Ỵ: "Y",
                Ƴ: "Y",
                Ɏ: "Y",
                Ỿ: "Y",
                "Ⓩ": "Z",
                Ｚ: "Z",
                Ź: "Z",
                Ẑ: "Z",
                Ż: "Z",
                Ž: "Z",
                Ẓ: "Z",
                Ẕ: "Z",
                Ƶ: "Z",
                Ȥ: "Z",
                "Ɀ": "Z",
                Ⱬ: "Z",
                Ꝣ: "Z",
                "ⓐ": "a",
                ａ: "a",
                ẚ: "a",
                à: "a",
                á: "a",
                â: "a",
                ầ: "a",
                ấ: "a",
                ẫ: "a",
                ẩ: "a",
                ã: "a",
                ā: "a",
                ă: "a",
                ằ: "a",
                ắ: "a",
                ẵ: "a",
                ẳ: "a",
                ȧ: "a",
                ǡ: "a",
                ä: "a",
                ǟ: "a",
                ả: "a",
                å: "a",
                ǻ: "a",
                ǎ: "a",
                ȁ: "a",
                ȃ: "a",
                ạ: "a",
                ậ: "a",
                ặ: "a",
                ḁ: "a",
                ą: "a",
                ⱥ: "a",
                ɐ: "a",
                ꜳ: "aa",
                æ: "ae",
                ǽ: "ae",
                ǣ: "ae",
                ꜵ: "ao",
                ꜷ: "au",
                ꜹ: "av",
                ꜻ: "av",
                ꜽ: "ay",
                "ⓑ": "b",
                ｂ: "b",
                ḃ: "b",
                ḅ: "b",
                ḇ: "b",
                ƀ: "b",
                ƃ: "b",
                ɓ: "b",
                "ⓒ": "c",
                ｃ: "c",
                ć: "c",
                ĉ: "c",
                ċ: "c",
                č: "c",
                ç: "c",
                ḉ: "c",
                ƈ: "c",
                ȼ: "c",
                ꜿ: "c",
                ↄ: "c",
                "ⓓ": "d",
                ｄ: "d",
                ḋ: "d",
                ď: "d",
                ḍ: "d",
                ḑ: "d",
                ḓ: "d",
                ḏ: "d",
                đ: "d",
                ƌ: "d",
                ɖ: "d",
                ɗ: "d",
                ꝺ: "d",
                ǳ: "dz",
                ǆ: "dz",
                "ⓔ": "e",
                ｅ: "e",
                è: "e",
                é: "e",
                ê: "e",
                ề: "e",
                ế: "e",
                ễ: "e",
                ể: "e",
                ẽ: "e",
                ē: "e",
                ḕ: "e",
                ḗ: "e",
                ĕ: "e",
                ė: "e",
                ë: "e",
                ẻ: "e",
                ě: "e",
                ȅ: "e",
                ȇ: "e",
                ẹ: "e",
                ệ: "e",
                ȩ: "e",
                ḝ: "e",
                ę: "e",
                ḙ: "e",
                ḛ: "e",
                ɇ: "e",
                ɛ: "e",
                ǝ: "e",
                "ⓕ": "f",
                ｆ: "f",
                ḟ: "f",
                ƒ: "f",
                ꝼ: "f",
                "ⓖ": "g",
                ｇ: "g",
                ǵ: "g",
                ĝ: "g",
                ḡ: "g",
                ğ: "g",
                ġ: "g",
                ǧ: "g",
                ģ: "g",
                ǥ: "g",
                ɠ: "g",
                "ꞡ": "g",
                ᵹ: "g",
                ꝿ: "g",
                "ⓗ": "h",
                ｈ: "h",
                ĥ: "h",
                ḣ: "h",
                ḧ: "h",
                ȟ: "h",
                ḥ: "h",
                ḩ: "h",
                ḫ: "h",
                ẖ: "h",
                ħ: "h",
                ⱨ: "h",
                ⱶ: "h",
                ɥ: "h",
                ƕ: "hv",
                "ⓘ": "i",
                ｉ: "i",
                ì: "i",
                í: "i",
                î: "i",
                ĩ: "i",
                ī: "i",
                ĭ: "i",
                ï: "i",
                ḯ: "i",
                ỉ: "i",
                ǐ: "i",
                ȉ: "i",
                ȋ: "i",
                ị: "i",
                į: "i",
                ḭ: "i",
                ɨ: "i",
                ı: "i",
                "ⓙ": "j",
                ｊ: "j",
                ĵ: "j",
                ǰ: "j",
                ɉ: "j",
                "ⓚ": "k",
                ｋ: "k",
                ḱ: "k",
                ǩ: "k",
                ḳ: "k",
                ķ: "k",
                ḵ: "k",
                ƙ: "k",
                ⱪ: "k",
                ꝁ: "k",
                ꝃ: "k",
                ꝅ: "k",
                "ꞣ": "k",
                "ⓛ": "l",
                ｌ: "l",
                ŀ: "l",
                ĺ: "l",
                ľ: "l",
                ḷ: "l",
                ḹ: "l",
                ļ: "l",
                ḽ: "l",
                ḻ: "l",
                ſ: "l",
                ł: "l",
                ƚ: "l",
                ɫ: "l",
                ⱡ: "l",
                ꝉ: "l",
                ꞁ: "l",
                ꝇ: "l",
                ǉ: "lj",
                "ⓜ": "m",
                ｍ: "m",
                ḿ: "m",
                ṁ: "m",
                ṃ: "m",
                ɱ: "m",
                ɯ: "m",
                "ⓝ": "n",
                ｎ: "n",
                ǹ: "n",
                ń: "n",
                ñ: "n",
                ṅ: "n",
                ň: "n",
                ṇ: "n",
                ņ: "n",
                ṋ: "n",
                ṉ: "n",
                ƞ: "n",
                ɲ: "n",
                ŉ: "n",
                "ꞑ": "n",
                "ꞥ": "n",
                ǌ: "nj",
                "ⓞ": "o",
                ｏ: "o",
                ò: "o",
                ó: "o",
                ô: "o",
                ồ: "o",
                ố: "o",
                ỗ: "o",
                ổ: "o",
                õ: "o",
                ṍ: "o",
                ȭ: "o",
                ṏ: "o",
                ō: "o",
                ṑ: "o",
                ṓ: "o",
                ŏ: "o",
                ȯ: "o",
                ȱ: "o",
                ö: "o",
                ȫ: "o",
                ỏ: "o",
                ő: "o",
                ǒ: "o",
                ȍ: "o",
                ȏ: "o",
                ơ: "o",
                ờ: "o",
                ớ: "o",
                ỡ: "o",
                ở: "o",
                ợ: "o",
                ọ: "o",
                ộ: "o",
                ǫ: "o",
                ǭ: "o",
                ø: "o",
                ǿ: "o",
                ɔ: "o",
                ꝋ: "o",
                ꝍ: "o",
                ɵ: "o",
                ƣ: "oi",
                ȣ: "ou",
                ꝏ: "oo",
                "ⓟ": "p",
                ｐ: "p",
                ṕ: "p",
                ṗ: "p",
                ƥ: "p",
                ᵽ: "p",
                ꝑ: "p",
                ꝓ: "p",
                ꝕ: "p",
                "ⓠ": "q",
                ｑ: "q",
                ɋ: "q",
                ꝗ: "q",
                ꝙ: "q",
                "ⓡ": "r",
                ｒ: "r",
                ŕ: "r",
                ṙ: "r",
                ř: "r",
                ȑ: "r",
                ȓ: "r",
                ṛ: "r",
                ṝ: "r",
                ŗ: "r",
                ṟ: "r",
                ɍ: "r",
                ɽ: "r",
                ꝛ: "r",
                "ꞧ": "r",
                ꞃ: "r",
                "ⓢ": "s",
                ｓ: "s",
                ß: "s",
                ś: "s",
                ṥ: "s",
                ŝ: "s",
                ṡ: "s",
                š: "s",
                ṧ: "s",
                ṣ: "s",
                ṩ: "s",
                ș: "s",
                ş: "s",
                ȿ: "s",
                "ꞩ": "s",
                ꞅ: "s",
                ẛ: "s",
                "ⓣ": "t",
                ｔ: "t",
                ṫ: "t",
                ẗ: "t",
                ť: "t",
                ṭ: "t",
                ț: "t",
                ţ: "t",
                ṱ: "t",
                ṯ: "t",
                ŧ: "t",
                ƭ: "t",
                ʈ: "t",
                ⱦ: "t",
                ꞇ: "t",
                ꜩ: "tz",
                "ⓤ": "u",
                ｕ: "u",
                ù: "u",
                ú: "u",
                û: "u",
                ũ: "u",
                ṹ: "u",
                ū: "u",
                ṻ: "u",
                ŭ: "u",
                ü: "u",
                ǜ: "u",
                ǘ: "u",
                ǖ: "u",
                ǚ: "u",
                ủ: "u",
                ů: "u",
                ű: "u",
                ǔ: "u",
                ȕ: "u",
                ȗ: "u",
                ư: "u",
                ừ: "u",
                ứ: "u",
                ữ: "u",
                ử: "u",
                ự: "u",
                ụ: "u",
                ṳ: "u",
                ų: "u",
                ṷ: "u",
                ṵ: "u",
                ʉ: "u",
                "ⓥ": "v",
                ｖ: "v",
                ṽ: "v",
                ṿ: "v",
                ʋ: "v",
                ꝟ: "v",
                ʌ: "v",
                ꝡ: "vy",
                "ⓦ": "w",
                ｗ: "w",
                ẁ: "w",
                ẃ: "w",
                ŵ: "w",
                ẇ: "w",
                ẅ: "w",
                ẘ: "w",
                ẉ: "w",
                ⱳ: "w",
                "ⓧ": "x",
                ｘ: "x",
                ẋ: "x",
                ẍ: "x",
                "ⓨ": "y",
                ｙ: "y",
                ỳ: "y",
                ý: "y",
                ŷ: "y",
                ỹ: "y",
                ȳ: "y",
                ẏ: "y",
                ÿ: "y",
                ỷ: "y",
                ẙ: "y",
                ỵ: "y",
                ƴ: "y",
                ɏ: "y",
                ỿ: "y",
                "ⓩ": "z",
                ｚ: "z",
                ź: "z",
                ẑ: "z",
                ż: "z",
                ž: "z",
                ẓ: "z",
                ẕ: "z",
                ƶ: "z",
                ȥ: "z",
                ɀ: "z",
                ⱬ: "z",
                ꝣ: "z"
            };
        K = a(document), I = function () {
            var a = 1;
            return function () {
                return a++
            }
        }(), K.on("mousemove", function (a) {
            M.x = a.pageX, M.y = a.pageY
        }), F = D(Object, {
            bind: function (a) {
                var b = this;
                return function () {
                    a.apply(b, arguments)
                }
            },
            init: function (c) {
                var d, e, g = ".select2-results";
                this.opts = c = this.prepareOpts(c), this.id = c.id, c.element.data("select2") !== b && null !== c.element.data("select2") && c.element.data("select2").destroy(), this.container = this.createContainer(), this.liveRegion = a("<span>", {
                    role: "status",
                    "aria-live": "polite"
                }).addClass("select2-hidden-accessible").appendTo(document.body), this.containerId = "s2id_" + (c.element.attr("id") || "autogen" + I()), this.containerEventName = this.containerId.replace(/([.])/g, "_").replace(/([;&,\-\.\+\*\~':"\!\^#$%@\[\]\(\)=>\|])/g, "\\$1"), this.container.attr("id", this.containerId), this.container.attr("title", c.element.attr("title")), this.body = a("body"), s(this.container, this.opts.element, this.opts.adaptContainerCssClass), this.container.attr("style", c.element.attr("style")), this.container.css(z(c.containerCss)), this.container.addClass(z(c.containerCssClass)), this.elementTabIndex = this.opts.element.attr("tabindex"), this.opts.element.data("select2", this).attr("tabindex", "-1").before(this.container).on("click.select2", p), this.container.data("select2", this), this.dropdown = this.container.find(".select2-drop"), s(this.dropdown, this.opts.element, this.opts.adaptDropdownCssClass), this.dropdown.addClass(z(c.dropdownCssClass)), this.dropdown.data("select2", this), this.dropdown.on("click", p), this.results = d = this.container.find(g), this.search = e = this.container.find("input.select2-input"), this.queryCount = 0, this.resultsPage = 0, this.context = null, this.initContainer(), this.container.on("click", p), k(this.results), this.dropdown.on("mousemove-filtered", g, this.bind(this.highlightUnderEvent)), this.dropdown.on("touchstart touchmove touchend", g, this.bind(function (a) {
                    this._touchEvent = !0, this.highlightUnderEvent(a)
                })), this.dropdown.on("touchmove", g, this.bind(this.touchMoved)), this.dropdown.on("touchstart touchend", g, this.bind(this.clearTouchMoved)), this.dropdown.on("click", this.bind(function () {
                    this._touchEvent && (this._touchEvent = !1, this.selectHighlighted())
                })), m(80, this.results), this.dropdown.on("scroll-debounced", g, this.bind(this.loadMoreIfNeeded)), a(this.container).on("change", ".select2-input", function (a) {
                    a.stopPropagation()
                }), a(this.dropdown).on("change", ".select2-input", function (a) {
                    a.stopPropagation()
                }), a.fn.mousewheel && d.mousewheel(function (a, b, c, e) {
                    var f = d.scrollTop();
                    e > 0 && 0 >= f - e ? (d.scrollTop(0), p(a)) : 0 > e && d.get(0).scrollHeight - d.scrollTop() + e <= d.height() && (d.scrollTop(d.get(0).scrollHeight - d.height()), p(a))
                }), j(e), e.on("keyup-change input paste", this.bind(this.updateResults)), e.on("focus", function () {
                    e.addClass("select2-focused")
                }), e.on("blur", function () {
                    e.removeClass("select2-focused")
                }), this.dropdown.on("mouseup", g, this.bind(function (b) {
                    a(b.target).closest(".select2-result-selectable").length > 0 && (this.highlightUnderEvent(b), this.selectHighlighted(b))
                })), this.dropdown.on("click mouseup mousedown touchstart touchend focusin", function (a) {
                    a.stopPropagation()
                }), this.nextSearchTerm = b, a.isFunction(this.opts.initSelection) && (this.initSelection(), this.monitorSource()), null !== c.maximumInputLength && this.search.attr("maxlength", c.maximumInputLength);
                var h = c.element.prop("disabled");
                h === b && (h = !1), this.enable(!h);
                var i = c.element.prop("readonly");
                i === b && (i = !1), this.readonly(i), L = L || f(), this.autofocus = c.element.prop("autofocus"), c.element.prop("autofocus", !1), this.autofocus && this.focus(), this.search.attr("placeholder", c.searchInputPlaceholder)
            },
            destroy: function () {
                var a = this.opts.element,
                    c = a.data("select2");
                this.close(), this.propertyObserver && (this.propertyObserver.disconnect(), this.propertyObserver = null), c !== b && (c.container.remove(), c.liveRegion.remove(), c.dropdown.remove(), a.removeClass("select2-offscreen").removeData("select2").off(".select2").prop("autofocus", this.autofocus || !1), this.elementTabIndex ? a.attr({
                    tabindex: this.elementTabIndex
                }) : a.removeAttr("tabindex"), a.show()), C.call(this, "container", "liveRegion", "dropdown", "results", "search")
            },
            optionToData: function (a) {
                return a.is("option") ? {
                    id: a.prop("value"),
                    text: a.text(),
                    element: a.get(),
                    css: a.attr("class"),
                    disabled: a.prop("disabled"),
                    locked: g(a.attr("locked"), "locked") || g(a.data("locked"), !0)
                } : a.is("optgroup") ? {
                    text: a.attr("label"),
                    children: [],
                    element: a.get(),
                    css: a.attr("class")
                } : void 0
            },
            prepareOpts: function (c) {
                var d, e, f, i, j = this;
                if (d = c.element, "select" === d.get(0).tagName.toLowerCase() && (this.select = e = c.element), e && a.each(["id", "multiple", "ajax", "query", "createSearchChoice", "initSelection", "data", "tags"], function () {
                        if (this in c) throw new Error("Option '" + this + "' is not allowed for Select2 when attached to a <select> element.")
                }), c = a.extend({}, {
                    populateResults: function (d, e, f) {
                            var g, h = this.opts.id,
                                i = this.liveRegion;
                            (g = function (d, e, k) {
                                var l, m, n, o, p, q, r, s, t, u;
                                for (d = c.sortResults(d, e, f), l = 0, m = d.length; m > l; l += 1) n = d[l], p = n.disabled === !0, o = !p && h(n) !== b, q = n.children && n.children.length > 0, r = a("<li></li>"), r.addClass("select2-results-dept-" + k), r.addClass("select2-result"), r.addClass(o ? "select2-result-selectable" : "select2-result-unselectable"), p && r.addClass("select2-disabled"), q && r.addClass("select2-result-with-children"), r.addClass(j.opts.formatResultCssClass(n)), r.attr("role", "presentation"), s = a(document.createElement("div")), s.addClass("select2-result-label"), s.attr("id", "select2-result-label-" + I()), s.attr("role", "option"), u = c.formatResult(n, s, f, j.opts.escapeMarkup), u !== b && (s.html(u), r.append(s)), q && (t = a("<ul></ul>"), t.addClass("select2-result-sub"), g(n.children, t, k + 1), r.append(t)), r.data("select2-data", n), e.append(r);
                                i.text(c.formatMatches(d.length))
                })(e, d, 0)
                }
                }, a.fn.select2.defaults, c), "function" != typeof c.id && (f = c.id, c.id = function (a) {
                        return a[f]
                }), a.isArray(c.element.data("select2Tags"))) {
                    if ("tags" in c) throw "tags specified as both an attribute 'data-select2-tags' and in options of Select2 " + c.element.attr("id");
                    c.tags = c.element.data("select2Tags")
                }
                if (e ? (c.query = this.bind(function (a) {
                        var c, e, f, g = {
                    results: [],
                    more: !1
                },
                            h = a.term;
                        f = function (b, c) {
                            var d;
                            b.is("option") ? a.matcher(h, b.text(), b) && c.push(j.optionToData(b)) : b.is("optgroup") && (d = j.optionToData(b), b.children().each2(function (a, b) {
                                f(b, d.children)
                }), d.children.length > 0 && c.push(d))
                }, c = d.children(), this.getPlaceholder() !== b && c.length > 0 && (e = this.getPlaceholderOption(), e && (c = c.not(e))), c.each2(function (a, b) {
                            f(b, g.results)
                }), a.callback(g)
                }), c.id = function (a) {
                        return a.id
                }) : "query" in c || ("ajax" in c ? (i = c.element.data("ajax-url"), i && i.length > 0 && (c.ajax.url = i), c.query = v.call(c.element, c.ajax)) : "data" in c ? c.query = w(c.data) : "tags" in c && (c.query = x(c.tags), c.createSearchChoice === b && (c.createSearchChoice = function (b) {
                        return {
                    id: a.trim(b),
                    text: a.trim(b)
                }
                }), c.initSelection === b && (c.initSelection = function (b, d) {
                        var e = [];
                        a(h(b.val(), c.separator)).each(function () {
                            var b = {
                    id: this,
                    text: this
                },
                                d = c.tags;
                            a.isFunction(d) && (d = d()), a(d).each(function () {
                                return g(this.id, b.id) ? (b = this, !1) : void 0
                }), e.push(b)
                }), d(e)
                }))), "function" != typeof c.query) throw "query function not defined for Select2 " + c.element.attr("id");
                if ("top" === c.createSearchChoicePosition) c.createSearchChoicePosition = function (a, b) {
                    a.unshift(b)
                };
                else if ("bottom" === c.createSearchChoicePosition) c.createSearchChoicePosition = function (a, b) {
                    a.push(b)
                };
                else if ("function" != typeof c.createSearchChoicePosition) throw "invalid createSearchChoicePosition option must be 'top', 'bottom' or a custom function";
                return c
            },
            monitorSource: function () {
                var a, c, d = this.opts.element;
                d.on("change.select2", this.bind(function () {
                    this.opts.element.data("select2-change-triggered") !== !0 && this.initSelection()
                })), a = this.bind(function () {
                    var a = d.prop("disabled");
                    a === b && (a = !1), this.enable(!a);
                    var c = d.prop("readonly");
                    c === b && (c = !1), this.readonly(c), s(this.container, this.opts.element, this.opts.adaptContainerCssClass), this.container.addClass(z(this.opts.containerCssClass)), s(this.dropdown, this.opts.element, this.opts.adaptDropdownCssClass), this.dropdown.addClass(z(this.opts.dropdownCssClass))
                }), d.length && d[0].attachEvent && d.each(function () {
                    this.attachEvent("onpropertychange", a)
                }), c = window.MutationObserver || window.WebKitMutationObserver || window.MozMutationObserver, c !== b && (this.propertyObserver && (delete this.propertyObserver, this.propertyObserver = null), this.propertyObserver = new c(function (b) {
                    b.forEach(a)
                }), this.propertyObserver.observe(d.get(0), {
                    attributes: !0,
                    subtree: !1
                }))
            },
            triggerSelect: function (b) {
                var c = a.Event("select2-selecting", {
                    val: this.id(b),
                    object: b
                });
                return this.opts.element.trigger(c), !c.isDefaultPrevented()
            },
            triggerChange: function (b) {
                b = b || {}, b = a.extend({}, b, {
                    type: "change",
                    val: this.val()
                }), this.opts.element.data("select2-change-triggered", !0), this.opts.element.trigger(b), this.opts.element.data("select2-change-triggered", !1), this.opts.element.click(), this.opts.blurOnChange && this.opts.element.blur()
            },
            isInterfaceEnabled: function () {
                return this.enabledInterface === !0
            },
            enableInterface: function () {
                var a = this._enabled && !this._readonly,
                    b = !a;
                return a === this.enabledInterface ? !1 : (this.container.toggleClass("select2-container-disabled", b), this.close(), this.enabledInterface = a, !0)
            },
            enable: function (a) {
                a === b && (a = !0), this._enabled !== a && (this._enabled = a, this.opts.element.prop("disabled", !a), this.enableInterface())
            },
            disable: function () {
                this.enable(!1)
            },
            readonly: function (a) {
                a === b && (a = !1), this._readonly !== a && (this._readonly = a, this.opts.element.prop("readonly", a), this.enableInterface())
            },
            opened: function () {
                return this.container.hasClass("select2-dropdown-open")
            },
            positionDropdown: function () {
                var b, c, d, e, f, g = this.dropdown,
                    h = this.container.offset(),
                    i = this.container.outerHeight(!1),
                    j = this.container.outerWidth(!1),
                    k = g.outerHeight(!1),
                    l = a(window),
                    m = l.width(),
                    n = l.height(),
                    o = l.scrollLeft() + m,
                    p = l.scrollTop() + n,
                    q = h.top + i,
                    r = h.left,
                    s = p >= q + k,
                    t = h.top - k >= l.scrollTop(),
                    u = g.outerWidth(!1),
                    v = o >= r + u,
                    w = g.hasClass("select2-drop-above");
                w ? (c = !0, !t && s && (d = !0, c = !1)) : (c = !1, !s && t && (d = !0, c = !0)), d && (g.hide(), h = this.container.offset(), i = this.container.outerHeight(!1), j = this.container.outerWidth(!1), k = g.outerHeight(!1), o = l.scrollLeft() + m, p = l.scrollTop() + n, q = h.top + i, r = h.left, u = g.outerWidth(!1), v = o >= r + u, g.show(), this.focusSearch()), this.opts.dropdownAutoWidth ? (f = a(".select2-results", g)[0], g.addClass("select2-drop-auto-width"), g.css("width", ""), u = g.outerWidth(!1) + (f.scrollHeight === f.clientHeight ? 0 : L.width), u > j ? j = u : u = j, k = g.outerHeight(!1), v = o >= r + u) : this.container.removeClass("select2-drop-auto-width"), "static" !== this.body.css("position") && (b = this.body.offset(), q -= b.top, r -= b.left), v || (r = h.left + this.container.outerWidth(!1) - u), e = {
                    left: r,
                    width: j
                }, c ? (e.top = h.top - k, e.bottom = "auto", this.container.addClass("select2-drop-above"), g.addClass("select2-drop-above")) : (e.top = q, e.bottom = "auto", this.container.removeClass("select2-drop-above"), g.removeClass("select2-drop-above")), e = a.extend(e, z(this.opts.dropdownCss)), g.css(e)
            },
            shouldOpen: function () {
                var b;
                return this.opened() ? !1 : this._enabled === !1 || this._readonly === !0 ? !1 : (b = a.Event("select2-opening"), this.opts.element.trigger(b), !b.isDefaultPrevented())
            },
            clearDropdownAlignmentPreference: function () {
                this.container.removeClass("select2-drop-above"), this.dropdown.removeClass("select2-drop-above")
            },
            open: function () {
                return this.shouldOpen() ? (this.opening(), !0) : !1
            },
            opening: function () {
                var b, d = this.containerEventName,
                    e = "scroll." + d,
                    f = "resize." + d,
                    g = "orientationchange." + d;
                this.container.addClass("select2-dropdown-open").addClass("select2-container-active"), this.clearDropdownAlignmentPreference(), this.dropdown[0] !== this.body.children().last()[0] && this.dropdown.detach().appendTo(this.body), b = a("#select2-drop-mask"), 0 == b.length && (b = a(document.createElement("div")), b.attr("id", "select2-drop-mask").attr("class", "select2-drop-mask"), b.hide(), b.appendTo(this.body), b.on("mousedown touchstart click", function (d) {
                    c(b);
                    var e, f = a("#select2-drop");
                    f.length > 0 && (e = f.data("select2"), e.opts.selectOnBlur && e.selectHighlighted({
                        noFocus: !0
                    }), e.close(), d.preventDefault(), d.stopPropagation())
                })), this.dropdown.prev()[0] !== b[0] && this.dropdown.before(b), a("#select2-drop").removeAttr("id"), this.dropdown.attr("id", "select2-drop"), b.show(), this.positionDropdown(), this.dropdown.show(), this.positionDropdown(), this.dropdown.addClass("select2-drop-active");
                var h = this;
                this.container.parents().add(window).each(function () {
                    a(this).on(f + " " + e + " " + g, function () {
                        h.opened() && h.positionDropdown()
                    })
                })
            },
            close: function () {
                if (this.opened()) {
                    var b = this.containerEventName,
                        c = "scroll." + b,
                        d = "resize." + b,
                        e = "orientationchange." + b;
                    this.container.parents().add(window).each(function () {
                        a(this).off(c).off(d).off(e)
                    }), this.clearDropdownAlignmentPreference(), a("#select2-drop-mask").hide(), this.dropdown.removeAttr("id"), this.dropdown.hide(), this.container.removeClass("select2-dropdown-open").removeClass("select2-container-active"), this.results.empty(), this.clearSearch(), this.search.removeClass("select2-active"), this.opts.element.trigger(a.Event("select2-close"))
                }
            },
            externalSearch: function (a) {
                this.open(), this.search.val(a), this.updateResults(!1)
            },
            clearSearch: function () { },
            getMaximumSelectionSize: function () {
                return z(this.opts.maximumSelectionSize)
            },
            ensureHighlightVisible: function () {
                var b, c, d, e, f, g, h, i = this.results;
                if (c = this.highlight(), !(0 > c)) {
                    if (0 == c) return void i.scrollTop(0);
                    b = this.findHighlightableChoices().find(".select2-result-label"), d = a(b[c]), e = d.offset().top + d.outerHeight(!0), c === b.length - 1 && (h = i.find("li.select2-more-results"), h.length > 0 && (e = h.offset().top + h.outerHeight(!0))), f = i.offset().top + i.outerHeight(!0), e > f && i.scrollTop(i.scrollTop() + (e - f)), g = d.offset().top - i.offset().top, 0 > g && "none" != d.css("display") && i.scrollTop(i.scrollTop() + g)
                }
            },
            findHighlightableChoices: function () {
                return this.results.find(".select2-result-selectable:not(.select2-disabled):not(.select2-selected)")
            },
            moveHighlight: function (b) {
                for (var c = this.findHighlightableChoices(), d = this.highlight() ; d > -1 && d < c.length;) {
                    d += b;
                    var e = a(c[d]);
                    if (e.hasClass("select2-result-selectable") && !e.hasClass("select2-disabled") && !e.hasClass("select2-selected")) {
                        this.highlight(d);
                        break
                    }
                }
            },
            highlight: function (b) {
                var c, d, f = this.findHighlightableChoices();
                return 0 === arguments.length ? e(f.filter(".select2-highlighted")[0], f.get()) : (b >= f.length && (b = f.length - 1), 0 > b && (b = 0), this.removeHighlight(), c = a(f[b]), c.addClass("select2-highlighted"), this.search.attr("aria-activedescendant", c.find(".select2-result-label").attr("id")), this.ensureHighlightVisible(), this.liveRegion.text(c.text()), d = c.data("select2-data"), void (d && this.opts.element.trigger({
                    type: "select2-highlight",
                    val: this.id(d),
                    choice: d
                })))
            },
            removeHighlight: function () {
                this.results.find(".select2-highlighted").removeClass("select2-highlighted")
            },
            touchMoved: function () {
                this._touchMoved = !0
            },
            clearTouchMoved: function () {
                this._touchMoved = !1
            },
            countSelectableResults: function () {
                return this.findHighlightableChoices().length
            },
            highlightUnderEvent: function (b) {
                var c = a(b.target).closest(".select2-result-selectable");
                if (c.length > 0 && !c.is(".select2-highlighted")) {
                    var d = this.findHighlightableChoices();
                    this.highlight(d.index(c))
                } else 0 == c.length && this.removeHighlight()
            },
            loadMoreIfNeeded: function () {
                var a, b = this.results,
                    c = b.find("li.select2-more-results"),
                    d = this.resultsPage + 1,
                    e = this,
                    f = this.search.val(),
                    g = this.context;
                0 !== c.length && (a = c.offset().top - b.offset().top - b.height(), a <= this.opts.loadMorePadding && (c.addClass("select2-active"), this.opts.query({
                    element: this.opts.element,
                    term: f,
                    page: d,
                    context: g,
                    matcher: this.opts.matcher,
                    callback: this.bind(function (a) {
                        e.opened() && (e.opts.populateResults.call(this, b, a.results, {
                            term: f,
                            page: d,
                            context: g
                        }), e.postprocessResults(a, !1, !1), a.more === !0 ? (c.detach().appendTo(b).text(z(e.opts.formatLoadMore, d + 1)), window.setTimeout(function () {
                            e.loadMoreIfNeeded()
                        }, 10)) : c.remove(), e.positionDropdown(), e.resultsPage = d, e.context = a.context, this.opts.element.trigger({
                            type: "select2-loaded",
                            items: a
                        }))
                    })
                })))
            },
            tokenize: function () { },
            updateResults: function (c) {
                function d() {
                    j.removeClass("select2-active"), m.positionDropdown(), m.liveRegion.text(k.find(".select2-no-results,.select2-selection-limit,.select2-searching").length ? k.text() : m.opts.formatMatches(k.find(".select2-result-selectable").length))
                }

                function e(a) {
                    k.html(a), d()
                }
                var f, h, i, j = this.search,
                    k = this.results,
                    l = this.opts,
                    m = this,
                    n = j.val(),
                    o = a.data(this.container, "select2-last-term");
                if ((c === !0 || !o || !g(n, o)) && (a.data(this.container, "select2-last-term", n), c === !0 || this.showSearchInput !== !1 && this.opened())) {
                    i = ++this.queryCount;
                    var p = this.getMaximumSelectionSize();
                    if (p >= 1 && (f = this.data(), a.isArray(f) && f.length >= p && y(l.formatSelectionTooBig, "formatSelectionTooBig"))) return void e("<li class='select2-selection-limit'>" + z(l.formatSelectionTooBig, p) + "</li>");
                    if (j.val().length < l.minimumInputLength) return e(y(l.formatInputTooShort, "formatInputTooShort") ? "<li class='select2-no-results'>" + z(l.formatInputTooShort, j.val(), l.minimumInputLength) + "</li>" : ""), void (c && this.showSearch && this.showSearch(!0));
                    if (l.maximumInputLength && j.val().length > l.maximumInputLength) return void e(y(l.formatInputTooLong, "formatInputTooLong") ? "<li class='select2-no-results'>" + z(l.formatInputTooLong, j.val(), l.maximumInputLength) + "</li>" : "");
                    l.formatSearching && 0 === this.findHighlightableChoices().length && e("<li class='select2-searching'>" + z(l.formatSearching) + "</li>"), j.addClass("select2-active"), this.removeHighlight(), h = this.tokenize(), h != b && null != h && j.val(h), this.resultsPage = 1, l.query({
                        element: l.element,
                        term: j.val(),
                        page: this.resultsPage,
                        context: null,
                        matcher: l.matcher,
                        callback: this.bind(function (f) {
                            var h;
                            if (i == this.queryCount) {
                                if (!this.opened()) return void this.search.removeClass("select2-active");
                                if (this.context = f.context === b ? null : f.context, this.opts.createSearchChoice && "" !== j.val() && (h = this.opts.createSearchChoice.call(m, j.val(), f.results), h !== b && null !== h && m.id(h) !== b && null !== m.id(h) && 0 === a(f.results).filter(function () {
                                        return g(m.id(this), m.id(h))
                                }).length && this.opts.createSearchChoicePosition(f.results, h)), 0 === f.results.length && y(l.formatNoMatches, "formatNoMatches")) return void e("<li class='select2-no-results'>" + z(l.formatNoMatches, j.val()) + "</li>");
                                k.empty(), m.opts.populateResults.call(this, k, f.results, {
                                    term: j.val(),
                                    page: this.resultsPage,
                                    context: null
                                }), f.more === !0 && y(l.formatLoadMore, "formatLoadMore") && (k.append("<li class='select2-more-results'>" + m.opts.escapeMarkup(z(l.formatLoadMore, this.resultsPage)) + "</li>"), window.setTimeout(function () {
                                    m.loadMoreIfNeeded()
                                }, 10)), this.postprocessResults(f, c), d(), this.opts.element.trigger({
                                    type: "select2-loaded",
                                    items: f
                                })
                            }
                        })
                    })
                }
            },
            cancel: function () {
                this.close()
            },
            blur: function () {
                this.opts.selectOnBlur && this.selectHighlighted({
                    noFocus: !0
                }), this.close(), this.container.removeClass("select2-container-active"), this.search[0] === document.activeElement && this.search.blur(), this.clearSearch(), this.selection.find(".select2-search-choice-focus").removeClass("select2-search-choice-focus")
            },
            focusSearch: function () {
                n(this.search)
            },
            selectHighlighted: function (a) {
                if (this._touchMoved) return void this.clearTouchMoved();
                var b = this.highlight(),
                    c = this.results.find(".select2-highlighted"),
                    d = c.closest(".select2-result").data("select2-data");
                d ? (this.highlight(b), this.onSelect(d, a)) : a && a.noFocus && this.close()
            },
            getPlaceholder: function () {
                var a;
                return this.opts.element.attr("placeholder") || this.opts.element.attr("data-placeholder") || this.opts.element.data("placeholder") || this.opts.placeholder || ((a = this.getPlaceholderOption()) !== b ? a.text() : b)
            },
            getPlaceholderOption: function () {
                if (this.select) {
                    var c = this.select.children("option").first();
                    if (this.opts.placeholderOption !== b) return "first" === this.opts.placeholderOption && c || "function" == typeof this.opts.placeholderOption && this.opts.placeholderOption(this.select);
                    if ("" === a.trim(c.text()) && "" === c.val()) return c
                }
            },
            initContainerWidth: function () {
                function c() {
                    var c, d, e, f, g, h;
                    if ("off" === this.opts.width) return null;
                    if ("element" === this.opts.width) return 0 === this.opts.element.outerWidth(!1) ? "auto" : this.opts.element.outerWidth(!1) + "px";
                    if ("copy" === this.opts.width || "resolve" === this.opts.width) {
                        if (c = this.opts.element.attr("style"), c !== b)
                            for (d = c.split(";"), f = 0, g = d.length; g > f; f += 1)
                                if (h = d[f].replace(/\s/g, ""), e = h.match(/^width:(([-+]?([0-9]*\.)?[0-9]+)(px|em|ex|%|in|cm|mm|pt|pc))/i), null !== e && e.length >= 1) return e[1];
                        return "resolve" === this.opts.width ? (c = this.opts.element.css("width"), c.indexOf("%") > 0 ? c : 0 === this.opts.element.outerWidth(!1) ? "auto" : this.opts.element.outerWidth(!1) + "px") : null
                    }
                    return a.isFunction(this.opts.width) ? this.opts.width() : this.opts.width
                }
                var d = c.call(this);
                null !== d && this.container.css("width", d)
            }
        }), G = D(F, {
            createContainer: function () {
                var b = a(document.createElement("div")).attr({
                    "class": "select2-container"
                }).html(["<a href='javascript:void(0)' class='select2-choice' tabindex='-1'>", "   <span class='select2-chosen'>&#160;</span><abbr class='select2-search-choice-close'></abbr>", "   <span class='select2-arrow' role='presentation'><b role='presentation'></b></span>", "</a>", "<label for='' class='select2-offscreen'></label>", "<input class='select2-focusser select2-offscreen' type='text' aria-haspopup='true' role='button' />", "<div class='select2-drop select2-display-none'>", "   <div class='select2-search'>", "       <label for='' class='select2-offscreen'></label>", "       <input type='text' autocomplete='off' autocorrect='off' autocapitalize='off' spellcheck='false' class='select2-input' role='combobox' aria-expanded='true'", "       aria-autocomplete='list' />", "   </div>", "   <ul class='select2-results' role='listbox'>", "   </ul>", "</div>"].join(""));
                return b
            },
            enableInterface: function () {
                this.parent.enableInterface.apply(this, arguments) && this.focusser.prop("disabled", !this.isInterfaceEnabled())
            },
            opening: function () {
                var c, d, e;
                this.opts.minimumResultsForSearch >= 0 && this.showSearch(!0), this.parent.opening.apply(this, arguments), this.showSearchInput !== !1 && this.search.val(this.focusser.val()), this.opts.shouldFocusInput(this) && (this.search.focus(), c = this.search.get(0), c.createTextRange ? (d = c.createTextRange(), d.collapse(!1), d.select()) : c.setSelectionRange && (e = this.search.val().length, c.setSelectionRange(e, e))), "" === this.search.val() && this.nextSearchTerm != b && (this.search.val(this.nextSearchTerm), this.search.select()), this.focusser.prop("disabled", !0).val(""), this.updateResults(!0), this.opts.element.trigger(a.Event("select2-open"))
            },
            close: function () {
                this.opened() && (this.parent.close.apply(this, arguments), this.focusser.prop("disabled", !1), this.opts.shouldFocusInput(this) && this.focusser.focus())
            },
            focus: function () {
                this.opened() ? this.close() : (this.focusser.prop("disabled", !1), this.opts.shouldFocusInput(this) && this.focusser.focus())
            },
            isFocused: function () {
                return this.container.hasClass("select2-container-active")
            },
            cancel: function () {
                this.parent.cancel.apply(this, arguments), this.focusser.prop("disabled", !1), this.opts.shouldFocusInput(this) && this.focusser.focus()
            },
            destroy: function () {
                a("label[for='" + this.focusser.attr("id") + "']").attr("for", this.opts.element.attr("id")), this.parent.destroy.apply(this, arguments), C.call(this, "selection", "focusser")
            },
            initContainer: function () {
                var b, d, e = this.container,
                    f = this.dropdown,
                    g = I();
                this.showSearch(this.opts.minimumResultsForSearch < 0 ? !1 : !0), this.selection = b = e.find(".select2-choice"), this.focusser = e.find(".select2-focusser"), b.find(".select2-chosen").attr("id", "select2-chosen-" + g), this.focusser.attr("aria-labelledby", "select2-chosen-" + g), this.results.attr("id", "select2-results-" + g), this.search.attr("aria-owns", "select2-results-" + g), this.focusser.attr("id", "s2id_autogen" + g), d = a("label[for='" + this.opts.element.attr("id") + "']"), this.focusser.prev().text(d.text()).attr("for", this.focusser.attr("id"));
                var h = this.opts.element.attr("title");
                this.opts.element.attr("title", h || d.text()), this.focusser.attr("tabindex", this.elementTabIndex), this.search.attr("id", this.focusser.attr("id") + "_search"), this.search.prev().text(a("label[for='" + this.focusser.attr("id") + "']").text()).attr("for", this.search.attr("id")), this.search.on("keydown", this.bind(function (a) {
                    if (this.isInterfaceEnabled()) {
                        if (a.which === E.PAGE_UP || a.which === E.PAGE_DOWN) return void p(a);
                        switch (a.which) {
                            case E.UP:
                            case E.DOWN:
                                return this.moveHighlight(a.which === E.UP ? -1 : 1), void p(a);
                            case E.ENTER:
                                return this.selectHighlighted(), void p(a);
                            case E.TAB:
                                return void this.selectHighlighted({
                                    noFocus: !0
                                });
                            case E.ESC:
                                return this.cancel(a), void p(a)
                        }
                    }
                })), this.search.on("blur", this.bind(function () {
                    document.activeElement === this.body.get(0) && window.setTimeout(this.bind(function () {
                        this.opened() && this.search.focus()
                    }), 0)
                })), this.focusser.on("keydown", this.bind(function (a) {
                    if (this.isInterfaceEnabled() && a.which !== E.TAB && !E.isControl(a) && !E.isFunctionKey(a) && a.which !== E.ESC) {
                        if (this.opts.openOnEnter === !1 && a.which === E.ENTER) return void p(a);
                        if (a.which == E.DOWN || a.which == E.UP || a.which == E.ENTER && this.opts.openOnEnter) {
                            if (a.altKey || a.ctrlKey || a.shiftKey || a.metaKey) return;
                            return this.open(), void p(a)
                        }
                        return a.which == E.DELETE || a.which == E.BACKSPACE ? (this.opts.allowClear && this.clear(), void p(a)) : void 0
                    }
                })), j(this.focusser), this.focusser.on("keyup-change input", this.bind(function (a) {
                    if (this.opts.minimumResultsForSearch >= 0) {
                        if (a.stopPropagation(), this.opened()) return;
                        this.open()
                    }
                })), b.on("mousedown touchstart", "abbr", this.bind(function (a) {
                    this.isInterfaceEnabled() && (this.clear(), q(a), this.close(), this.selection.focus())
                })), b.on("mousedown touchstart", this.bind(function (d) {
                    c(b), this.container.hasClass("select2-container-active") || this.opts.element.trigger(a.Event("select2-focus")), this.opened() ? this.close() : this.isInterfaceEnabled() && this.open(), p(d)
                })), f.on("mousedown touchstart", this.bind(function () {
                    this.opts.shouldFocusInput(this) && this.search.focus()
                })), b.on("focus", this.bind(function (a) {
                    p(a)
                })), this.focusser.on("focus", this.bind(function () {
                    this.container.hasClass("select2-container-active") || this.opts.element.trigger(a.Event("select2-focus")), this.container.addClass("select2-container-active")
                })).on("blur", this.bind(function () {
                    this.opened() || (this.container.removeClass("select2-container-active"), this.opts.element.trigger(a.Event("select2-blur")))
                })), this.search.on("focus", this.bind(function () {
                    this.container.hasClass("select2-container-active") || this.opts.element.trigger(a.Event("select2-focus")), this.container.addClass("select2-container-active")
                })), this.initContainerWidth(), this.opts.element.addClass("select2-offscreen"), this.setPlaceholder()
            },
            clear: function (b) {
                var c = this.selection.data("select2-data");
                if (c) {
                    var d = a.Event("select2-clearing");
                    if (this.opts.element.trigger(d), d.isDefaultPrevented()) return;
                    var e = this.getPlaceholderOption();
                    this.opts.element.val(e ? e.val() : ""), this.selection.find(".select2-chosen").empty(), this.selection.removeData("select2-data"), this.setPlaceholder(), b !== !1 && (this.opts.element.trigger({
                        type: "select2-removed",
                        val: this.id(c),
                        choice: c
                    }), this.triggerChange({
                        removed: c
                    }))
                }
            },
            initSelection: function () {
                if (this.isPlaceholderOptionSelected()) this.updateSelection(null), this.close(), this.setPlaceholder();
                else {
                    var a = this;
                    this.opts.initSelection.call(null, this.opts.element, function (c) {
                        c !== b && null !== c && (a.updateSelection(c), a.close(), a.setPlaceholder(), a.nextSearchTerm = a.opts.nextSearchTerm(c, a.search.val()))
                    })
                }
            },
            isPlaceholderOptionSelected: function () {
                var a;
                return this.getPlaceholder() === b ? !1 : (a = this.getPlaceholderOption()) !== b && a.prop("selected") || "" === this.opts.element.val() || this.opts.element.val() === b || null === this.opts.element.val()
            },
            prepareOpts: function () {
                var b = this.parent.prepareOpts.apply(this, arguments),
                    c = this;
                return "select" === b.element.get(0).tagName.toLowerCase() ? b.initSelection = function (a, b) {
                    var d = a.find("option").filter(function () {
                        return this.selected && !this.disabled
                    });
                    b(c.optionToData(d))
                } : "data" in b && (b.initSelection = b.initSelection || function (c, d) {
                    var e = c.val(),
                        f = null;
                    b.query({
                        matcher: function (a, c, d) {
                            var h = g(e, b.id(d));
                            return h && (f = d), h
                        },
                        callback: a.isFunction(d) ? function () {
                            d(f)
                        } : a.noop
                    })
                }), b
            },
            getPlaceholder: function () {
                return this.select && this.getPlaceholderOption() === b ? b : this.parent.getPlaceholder.apply(this, arguments)
            },
            setPlaceholder: function () {
                var a = this.getPlaceholder();
                if (this.isPlaceholderOptionSelected() && a !== b) {
                    if (this.select && this.getPlaceholderOption() === b) return;
                    this.selection.find(".select2-chosen").html(this.opts.escapeMarkup(a)), this.selection.addClass("select2-default"), this.container.removeClass("select2-allowclear")
                }
            },
            postprocessResults: function (a, b, c) {
                var d = 0,
                    e = this;
                if (this.findHighlightableChoices().each2(function (a, b) {
                        return g(e.id(b.data("select2-data")), e.opts.element.val()) ? (d = a, !1) : void 0
                }), c !== !1 && this.highlight(b === !0 && d >= 0 ? d : 0), b === !0) {
                    var f = this.opts.minimumResultsForSearch;
                    f >= 0 && this.showSearch(A(a.results) >= f)
                }
            },
            showSearch: function (b) {
                this.showSearchInput !== b && (this.showSearchInput = b, this.dropdown.find(".select2-search").toggleClass("select2-search-hidden", !b), this.dropdown.find(".select2-search").toggleClass("select2-offscreen", !b), a(this.dropdown, this.container).toggleClass("select2-with-searchbox", b))
            },
            onSelect: function (a, b) {
                if (this.triggerSelect(a)) {
                    var c = this.opts.element.val(),
                        d = this.data();
                    this.opts.element.val(this.id(a)), this.updateSelection(a), this.opts.element.trigger({
                        type: "select2-selected",
                        val: this.id(a),
                        choice: a
                    }), this.nextSearchTerm = this.opts.nextSearchTerm(a, this.search.val()), this.close(), b && b.noFocus || !this.opts.shouldFocusInput(this) || this.focusser.focus(), g(c, this.id(a)) || this.triggerChange({
                        added: a,
                        removed: d
                    })
                }
            },
            updateSelection: function (a) {
                var c, d, e = this.selection.find(".select2-chosen");
                this.selection.data("select2-data", a), e.empty(), null !== a && (c = this.opts.formatSelection(a, e, this.opts.escapeMarkup)), c !== b && e.append(c), d = this.opts.formatSelectionCssClass(a, e), d !== b && e.addClass(d), this.selection.removeClass("select2-default"), this.opts.allowClear && this.getPlaceholder() !== b && this.container.addClass("select2-allowclear")
            },
            val: function () {
                var a, c = !1,
                    d = null,
                    e = this,
                    f = this.data();
                if (0 === arguments.length) return this.opts.element.val();
                if (a = arguments[0], arguments.length > 1 && (c = arguments[1]), this.select) this.select.val(a).find("option").filter(function () {
                    return this.selected
                }).each2(function (a, b) {
                    return d = e.optionToData(b), !1
                }), this.updateSelection(d), this.setPlaceholder(), c && this.triggerChange({
                    added: d,
                    removed: f
                });
                else {
                    if (!a && 0 !== a) return void this.clear(c);
                    if (this.opts.initSelection === b) throw new Error("cannot call val() if initSelection() is not defined");
                    this.opts.element.val(a), this.opts.initSelection(this.opts.element, function (a) {
                        e.opts.element.val(a ? e.id(a) : ""), e.updateSelection(a), e.setPlaceholder(), c && e.triggerChange({
                            added: a,
                            removed: f
                        })
                    })
                }
            },
            clearSearch: function () {
                this.search.val(""), this.focusser.val("")
            },
            data: function (a) {
                var c, d = !1;
                return 0 === arguments.length ? (c = this.selection.data("select2-data"), c == b && (c = null), c) : (arguments.length > 1 && (d = arguments[1]), void (a ? (c = this.data(), this.opts.element.val(a ? this.id(a) : ""), this.updateSelection(a), d && this.triggerChange({
                    added: a,
                    removed: c
                })) : this.clear(d)))
            }
        }), H = D(F, {
            createContainer: function () {
                var b = a(document.createElement("div")).attr({
                    "class": "select2-container select2-container-multi"
                }).html(["<ul class='select2-choices'>", "  <li class='select2-search-field'>", "    <label for='' class='select2-offscreen'></label>", "    <input type='text' autocomplete='off' autocorrect='off' autocapitalize='off' spellcheck='false' class='select2-input'>", "  </li>", "</ul>", "<div class='select2-drop select2-drop-multi select2-display-none'>", "   <ul class='select2-results'>", "   </ul>", "</div>"].join(""));
                return b
            },
            prepareOpts: function () {
                var b = this.parent.prepareOpts.apply(this, arguments),
                    c = this;
                return "select" === b.element.get(0).tagName.toLowerCase() ? b.initSelection = function (a, b) {
                    var d = [];
                    a.find("option").filter(function () {
                        return this.selected && !this.disabled
                    }).each2(function (a, b) {
                        d.push(c.optionToData(b))
                    }), b(d)
                } : "data" in b && (b.initSelection = b.initSelection || function (c, d) {
                    var e = h(c.val(), b.separator),
                        f = [];
                    b.query({
                        matcher: function (c, d, h) {
                            var i = a.grep(e, function (a) {
                                return g(a, b.id(h))
                            }).length;
                            return i && f.push(h), i
                        },
                        callback: a.isFunction(d) ? function () {
                            for (var a = [], c = 0; c < e.length; c++)
                                for (var h = e[c], i = 0; i < f.length; i++) {
                                    var j = f[i];
                                    if (g(h, b.id(j))) {
                                        a.push(j), f.splice(i, 1);
                                        break
                                    }
                                }
                            d(a)
                        } : a.noop
                    })
                }), b
            },
            selectChoice: function (a) {
                var b = this.container.find(".select2-search-choice-focus");
                b.length && a && a[0] == b[0] || (b.length && this.opts.element.trigger("choice-deselected", b), b.removeClass("select2-search-choice-focus"), a && a.length && (this.close(), a.addClass("select2-search-choice-focus"), this.opts.element.trigger("choice-selected", a)))
            },
            destroy: function () {
                a("label[for='" + this.search.attr("id") + "']").attr("for", this.opts.element.attr("id")), this.parent.destroy.apply(this, arguments), C.call(this, "searchContainer", "selection")
            },
            initContainer: function () {
                var b, c = ".select2-choices";
                this.searchContainer = this.container.find(".select2-search-field"), this.selection = b = this.container.find(c);
                var d = this;
                this.selection.on("click", ".select2-search-choice:not(.select2-locked)", function () {
                    d.search[0].focus(), d.selectChoice(a(this))
                }), this.search.attr("id", "s2id_autogen" + I()), this.search.prev().text(a("label[for='" + this.opts.element.attr("id") + "']").text()).attr("for", this.search.attr("id")), this.search.on("input paste", this.bind(function () {
                    this.isInterfaceEnabled() && (this.opened() || this.open())
                })), this.search.attr("tabindex", this.elementTabIndex), this.keydowns = 0, this.search.on("keydown", this.bind(function (a) {
                    if (this.isInterfaceEnabled()) {
                        ++this.keydowns;
                        var c = b.find(".select2-search-choice-focus"),
                            d = c.prev(".select2-search-choice:not(.select2-locked)"),
                            e = c.next(".select2-search-choice:not(.select2-locked)"),
                            f = o(this.search);
                        if (c.length && (a.which == E.LEFT || a.which == E.RIGHT || a.which == E.BACKSPACE || a.which == E.DELETE || a.which == E.ENTER)) {
                            var g = c;
                            return a.which == E.LEFT && d.length ? g = d : a.which == E.RIGHT ? g = e.length ? e : null : a.which === E.BACKSPACE ? this.unselect(c.first()) && (this.search.width(10), g = d.length ? d : e) : a.which == E.DELETE ? this.unselect(c.first()) && (this.search.width(10), g = e.length ? e : null) : a.which == E.ENTER && (g = null), this.selectChoice(g), p(a), void (g && g.length || this.open())
                        }
                        if ((a.which === E.BACKSPACE && 1 == this.keydowns || a.which == E.LEFT) && 0 == f.offset && !f.length) return this.selectChoice(b.find(".select2-search-choice:not(.select2-locked)").last()), void p(a);
                        if (this.selectChoice(null), this.opened()) switch (a.which) {
                            case E.UP:
                            case E.DOWN:
                                return this.moveHighlight(a.which === E.UP ? -1 : 1), void p(a);
                            case E.ENTER:
                                return this.selectHighlighted(), void p(a);
                            case E.TAB:
                                return this.selectHighlighted({
                                    noFocus: !0
                                }), void this.close();
                            case E.ESC:
                                return this.cancel(a), void p(a)
                        }
                        if (a.which !== E.TAB && !E.isControl(a) && !E.isFunctionKey(a) && a.which !== E.BACKSPACE && a.which !== E.ESC) {
                            if (a.which === E.ENTER) {
                                if (this.opts.openOnEnter === !1) return;
                                if (a.altKey || a.ctrlKey || a.shiftKey || a.metaKey) return
                            }
                            this.open(), (a.which === E.PAGE_UP || a.which === E.PAGE_DOWN) && p(a), a.which === E.ENTER && p(a)
                        }
                    }
                })), this.search.on("keyup", this.bind(function () {
                    this.keydowns = 0, this.resizeSearch()
                })), this.search.on("blur", this.bind(function (b) {
                    this.container.removeClass("select2-container-active"), this.search.removeClass("select2-focused"), this.selectChoice(null), this.opened() || this.clearSearch(), b.stopImmediatePropagation(), this.opts.element.trigger(a.Event("select2-blur"))
                })), this.container.on("click", c, this.bind(function (b) {
                    this.isInterfaceEnabled() && (a(b.target).closest(".select2-search-choice").length > 0 || (this.selectChoice(null), this.clearPlaceholder(), this.container.hasClass("select2-container-active") || this.opts.element.trigger(a.Event("select2-focus")), this.open(), this.focusSearch(), b.preventDefault()))
                })), this.container.on("focus", c, this.bind(function () {
                    this.isInterfaceEnabled() && (this.container.hasClass("select2-container-active") || this.opts.element.trigger(a.Event("select2-focus")), this.container.addClass("select2-container-active"), this.dropdown.addClass("select2-drop-active"), this.clearPlaceholder())
                })), this.initContainerWidth(), this.opts.element.addClass("select2-offscreen"), this.clearSearch()
            },
            enableInterface: function () {
                this.parent.enableInterface.apply(this, arguments) && this.search.prop("disabled", !this.isInterfaceEnabled())
            },
            initSelection: function () {
                if ("" === this.opts.element.val() && "" === this.opts.element.text() && (this.updateSelection([]), this.close(), this.clearSearch()), this.select || "" !== this.opts.element.val()) {
                    var a = this;
                    this.opts.initSelection.call(null, this.opts.element, function (c) {
                        c !== b && null !== c && (a.updateSelection(c), a.close(), a.clearSearch())
                    })
                }
            },
            clearSearch: function () {
                var a = this.getPlaceholder(),
                    c = this.getMaxSearchWidth();
                a !== b && 0 === this.getVal().length && this.search.hasClass("select2-focused") === !1 ? (this.search.val(a).addClass("select2-default"), this.search.width(c > 0 ? c : this.container.css("width"))) : this.search.val("").width(10)
            },
            clearPlaceholder: function () {
                this.search.hasClass("select2-default") && this.search.val("").removeClass("select2-default")
            },
            opening: function () {
                this.clearPlaceholder(), this.resizeSearch(), this.parent.opening.apply(this, arguments), this.focusSearch(), "" === this.search.val() && this.nextSearchTerm != b && (this.search.val(this.nextSearchTerm), this.search.select()), this.updateResults(!0), this.opts.shouldFocusInput(this) && this.search.focus(), this.opts.element.trigger(a.Event("select2-open"))
            },
            close: function () {
                this.opened() && this.parent.close.apply(this, arguments)
            },
            focus: function () {
                this.close(), this.search.focus()
            },
            isFocused: function () {
                return this.search.hasClass("select2-focused")
            },
            updateSelection: function (b) {
                var c = [],
                    d = [],
                    f = this;
                a(b).each(function () {
                    e(f.id(this), c) < 0 && (c.push(f.id(this)), d.push(this))
                }), b = d, this.selection.find(".select2-search-choice").remove(), a(b).each(function () {
                    f.addSelectedChoice(this)
                }), f.postprocessResults()
            },
            tokenize: function () {
                var a = this.search.val();
                a = this.opts.tokenizer.call(this, a, this.data(), this.bind(this.onSelect), this.opts), null != a && a != b && (this.search.val(a), a.length > 0 && this.open())
            },
            onSelect: function (a, c) {
                this.triggerSelect(a) && (this.addSelectedChoice(a), this.opts.element.trigger({
                    type: "selected",
                    val: this.id(a),
                    choice: a
                }), this.nextSearchTerm = this.opts.nextSearchTerm(a, this.search.val()), this.clearSearch(), this.updateResults(), (this.select || !this.opts.closeOnSelect) && this.postprocessResults(a, !1, this.opts.closeOnSelect === !0), this.opts.closeOnSelect ? (this.close(), this.search.width(10)) : this.countSelectableResults() > 0 ? (this.search.width(10), this.resizeSearch(), this.getMaximumSelectionSize() > 0 && this.val().length >= this.getMaximumSelectionSize() ? this.updateResults(!0) : this.nextSearchTerm != b && (this.search.val(this.nextSearchTerm), this.updateResults(), this.search.select()), this.positionDropdown()) : (this.close(), this.search.width(10)), this.triggerChange({
                    added: a
                }), c && c.noFocus || this.focusSearch())
            },
            cancel: function () {
                this.close(), this.focusSearch()
            },
            addSelectedChoice: function (c) {
                var d, e, f = !c.locked,
                    g = a("<li class='select2-search-choice'>    <div></div>    <a href='#' class='select2-search-choice-close' tabindex='-1'></a></li>"),
                    h = a("<li class='select2-search-choice select2-locked'><div></div></li>"),
                    i = f ? g : h,
                    j = this.id(c),
                    k = this.getVal();
                d = this.opts.formatSelection(c, i.find("div"), this.opts.escapeMarkup), d != b && i.find("div").replaceWith("<div>" + d + "</div>"), e = this.opts.formatSelectionCssClass(c, i.find("div")), e != b && i.addClass(e), f && i.find(".select2-search-choice-close").on("mousedown", p).on("click dblclick", this.bind(function (b) {
                    this.isInterfaceEnabled() && (this.unselect(a(b.target)), this.selection.find(".select2-search-choice-focus").removeClass("select2-search-choice-focus"), p(b), this.close(), this.focusSearch())
                })).on("focus", this.bind(function () {
                    this.isInterfaceEnabled() && (this.container.addClass("select2-container-active"), this.dropdown.addClass("select2-drop-active"))
                })), i.data("select2-data", c), i.insertBefore(this.searchContainer), k.push(j), this.setVal(k)
            },
            unselect: function (b) {
                var c, d, f = this.getVal();
                if (b = b.closest(".select2-search-choice"), 0 === b.length) throw "Invalid argument: " + b + ". Must be .select2-search-choice";
                if (c = b.data("select2-data")) {
                    var g = a.Event("select2-removing");
                    if (g.val = this.id(c), g.choice = c, this.opts.element.trigger(g), g.isDefaultPrevented()) return !1;
                    for (;
                        (d = e(this.id(c), f)) >= 0;) f.splice(d, 1), this.setVal(f), this.select && this.postprocessResults();
                    return b.remove(), this.opts.element.trigger({
                        type: "select2-removed",
                        val: this.id(c),
                        choice: c
                    }), this.triggerChange({
                        removed: c
                    }), !0
                }
            },
            postprocessResults: function (a, b, c) {
                var d = this.getVal(),
                    f = this.results.find(".select2-result"),
                    g = this.results.find(".select2-result-with-children"),
                    h = this;
                f.each2(function (a, b) {
                    var c = h.id(b.data("select2-data"));
                    e(c, d) >= 0 && (b.addClass("select2-selected"), b.find(".select2-result-selectable").addClass("select2-selected"))
                }), g.each2(function (a, b) {
                    b.is(".select2-result-selectable") || 0 !== b.find(".select2-result-selectable:not(.select2-selected)").length || b.addClass("select2-selected")
                }), -1 == this.highlight() && c !== !1 && h.highlight(0), !this.opts.createSearchChoice && !f.filter(".select2-result:not(.select2-selected)").length > 0 && (!a || a && !a.more && 0 === this.results.find(".select2-no-results").length) && y(h.opts.formatNoMatches, "formatNoMatches") && this.results.append("<li class='select2-no-results'>" + z(h.opts.formatNoMatches, h.search.val()) + "</li>")
            },
            getMaxSearchWidth: function () {
                return this.selection.width() - i(this.search)
            },
            resizeSearch: function () {
                var a, b, c, d, e, f = i(this.search);
                a = r(this.search) + 10, b = this.search.offset().left, c = this.selection.width(), d = this.selection.offset().left, e = c - (b - d) - f, a > e && (e = c - f), 40 > e && (e = c - f), 0 >= e && (e = a), this.search.width(Math.floor(e))
            },
            getVal: function () {
                var a;
                return this.select ? (a = this.select.val(), null === a ? [] : a) : (a = this.opts.element.val(), h(a, this.opts.separator))
            },
            setVal: function (b) {
                var c;
                this.select ? this.select.val(b) : (c = [], a(b).each(function () {
                    e(this, c) < 0 && c.push(this)
                }), this.opts.element.val(0 === c.length ? "" : c.join(this.opts.separator)))
            },
            buildChangeDetails: function (a, b) {
                for (var b = b.slice(0), a = a.slice(0), c = 0; c < b.length; c++)
                    for (var d = 0; d < a.length; d++) g(this.opts.id(b[c]), this.opts.id(a[d])) && (b.splice(c, 1), c > 0 && c--, a.splice(d, 1), d--);
                return {
                    added: b,
                    removed: a
                }
            },
            val: function (c, d) {
                var e, f = this;
                if (0 === arguments.length) return this.getVal();
                if (e = this.data(), e.length || (e = []), !c && 0 !== c) return this.opts.element.val(""), this.updateSelection([]), this.clearSearch(), void (d && this.triggerChange({
                    added: this.data(),
                    removed: e
                }));
                if (this.setVal(c), this.select) this.opts.initSelection(this.select, this.bind(this.updateSelection)), d && this.triggerChange(this.buildChangeDetails(e, this.data()));
                else {
                    if (this.opts.initSelection === b) throw new Error("val() cannot be called if initSelection() is not defined");
                    this.opts.initSelection(this.opts.element, function (b) {
                        var c = a.map(b, f.id);
                        f.setVal(c), f.updateSelection(b), f.clearSearch(), d && f.triggerChange(f.buildChangeDetails(e, f.data()))
                    })
                }
                this.clearSearch()
            },
            onSortStart: function () {
                if (this.select) throw new Error("Sorting of elements is not supported when attached to <select>. Attach to <input type='hidden'/> instead.");
                this.search.width(0), this.searchContainer.hide()
            },
            onSortEnd: function () {
                var b = [],
                    c = this;
                this.searchContainer.show(), this.searchContainer.appendTo(this.searchContainer.parent()), this.resizeSearch(), this.selection.find(".select2-search-choice").each(function () {
                    b.push(c.opts.id(a(this).data("select2-data")))
                }), this.setVal(b), this.triggerChange()
            },
            data: function (b, c) {
                var d, e, f = this;
                return 0 === arguments.length ? this.selection.children(".select2-search-choice").map(function () {
                    return a(this).data("select2-data")
                }).get() : (e = this.data(), b || (b = []), d = a.map(b, function (a) {
                    return f.opts.id(a)
                }), this.setVal(d), this.updateSelection(b), this.clearSearch(), c && this.triggerChange(this.buildChangeDetails(e, this.data())), void 0)
            }
        }), a.fn.select2 = function () {
            var c, d, f, g, h, i = Array.prototype.slice.call(arguments, 0),
                j = ["val", "destroy", "opened", "open", "close", "focus", "isFocused", "container", "dropdown", "onSortStart", "onSortEnd", "enable", "disable", "readonly", "positionDropdown", "data", "search"],
                k = ["opened", "isFocused", "container", "dropdown"],
                l = ["val", "data"],
                m = {
                    search: "externalSearch"
                };
            return this.each(function () {
                if (0 === i.length || "object" == typeof i[0]) c = 0 === i.length ? {} : a.extend({}, i[0]), c.element = a(this), "select" === c.element.get(0).tagName.toLowerCase() ? h = c.element.prop("multiple") : (h = c.multiple || !1, "tags" in c && (c.multiple = h = !0)), d = h ? new window.Select2["class"].multi : new window.Select2["class"].single, d.init(c);
                else {
                    if ("string" != typeof i[0]) throw "Invalid arguments to select2 plugin: " + i;
                    if (e(i[0], j) < 0) throw "Unknown method: " + i[0];
                    if (g = b, d = a(this).data("select2"), d === b) return;
                    if (f = i[0], "container" === f ? g = d.container : "dropdown" === f ? g = d.dropdown : (m[f] && (f = m[f]), g = d[f].apply(d, i.slice(1))), e(i[0], k) >= 0 || e(i[0], l) >= 0 && 1 == i.length) return !1
                }
            }), g === b ? this : g
        }, a.fn.select2.defaults = {
            width: "copy",
            loadMorePadding: 0,
            closeOnSelect: !0,
            openOnEnter: !0,
            containerCss: {},
            dropdownCss: {},
            containerCssClass: "",
            dropdownCssClass: "",
            formatResult: function (a, b, c, d) {
                var e = [];
                return t(a.text, c.term, e, d), e.join("")
            },
            formatSelection: function (a, c, d) {
                return a ? d(a.text) : b
            },
            sortResults: function (a) {
                return a
            },
            formatResultCssClass: function (a) {
                return a.css
            },
            formatSelectionCssClass: function () {
                return b
            },
            formatMatches: function (a) {
                return a + " results are available, use up and down arrow keys to navigate."
            },
            formatNoMatches: function () {
                return "No matches found"
            },
            formatInputTooShort: function (a, b) {
                var c = b - a.length;
                return "Please enter " + c + " or more character" + (1 == c ? "" : "s")
            },
            formatInputTooLong: function (a, b) {
                var c = a.length - b;
                return "Please delete " + c + " character" + (1 == c ? "" : "s")
            },
            formatSelectionTooBig: function (a) {
                return "You can only select " + a + " item" + (1 == a ? "" : "s")
            },
            formatLoadMore: function () {
                return "Loading more results…"
            },
            formatSearching: function () {
                return "Searching…"
            },
            minimumResultsForSearch: 0,
            minimumInputLength: 0,
            maximumInputLength: null,
            maximumSelectionSize: 0,
            id: function (a) {
                return a == b ? null : a.id
            },
            matcher: function (a, b) {
                return d("" + b).toUpperCase().indexOf(d("" + a).toUpperCase()) >= 0
            },
            separator: ",",
            tokenSeparators: [],
            tokenizer: B,
            escapeMarkup: u,
            blurOnChange: !1,
            selectOnBlur: !1,
            adaptContainerCssClass: function (a) {
                return a
            },
            adaptDropdownCssClass: function () {
                return null
            },
            nextSearchTerm: function () {
                return b
            },
            searchInputPlaceholder: "",
            createSearchChoicePosition: "top",
            shouldFocusInput: function (a) {
                var b = "ontouchstart" in window || navigator.msMaxTouchPoints > 0;
                return b && a.opts.minimumResultsForSearch < 0 ? !1 : !0
            }
        }, a.fn.select2.ajaxDefaults = {
            transport: a.ajax,
            params: {
                type: "GET",
                cache: !1,
                dataType: "json"
            }
        }, window.Select2 = {
            query: {
                ajax: v,
                local: w,
                tags: x
            },
            util: {
                debounce: l,
                markMatch: t,
                escapeMarkup: u,
                stripDiacritics: d
            },
            "class": {
                "abstract": F,
                single: G,
                multi: H
            }
        }
    }
}(jQuery);