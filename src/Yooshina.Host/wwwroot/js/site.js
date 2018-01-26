// Write your Javascript code.


$(function () {
    if ($("yooshinamenu").length > 0) {
        if (localStorage.getItem('yooshinamenu') === 'active') {
            $('nav#yooshinamenu').addClass('active');
            setTimeout(function () { $("nav#yooshinamenu").toggleClass("in"); }, 200);
            $("a#toranj-toogle-menu").addClass('isopen');
        }

        $("li.haschilds a").click(function () {
            $(this).parent().toggleClass("isopen");
        });

        $("a#yooshina-toogle-menu").click(function () {
            $(this).toggleClass("isopen");
            $("nav#yooshinamenu").toggleClass("active");
            if ($("nav#yooshinamenu").hasClass("active")) {
                setTimeout(function () { $("nav#yooshinamenu").toggleClass("in"); }, 200);
            } else {
                $("nav#yooshinamenu").toggleClass("in");
            }

            if ($('nav#yooshinamenu').hasClass('active')) {
                localStorage.setItem('toranjmenu', 'active');
            } else {
                localStorage.setItem('toranjmenu', '');
            }

        });
    }
});