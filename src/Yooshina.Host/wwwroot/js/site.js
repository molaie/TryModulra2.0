// Write your Javascript code.


$(function () {
    if ($("#yooshinanav").length > 0) {
        if (localStorage.getItem('yooshinamenu') === 'active') {
            $('nav#yooshinanav').addClass('active');
            setTimeout(function () { $("nav#yooshinanav").toggleClass("in"); }, 200);
            $("a#toranj-toogle-menu").addClass('isopen');
        }

        $("li.haschilds a").click(function () {
            $(this).parent().toggleClass("isopen");
        });

        $("a#yooshina-toogle-menu").click(function () {
            $(this).toggleClass("isopen");
            $("nav#yooshinanav").toggleClass("active");
            if ($("nav#yooshinanav").hasClass("active")) {
                setTimeout(function () { $("nav#yooshinanav").toggleClass("in"); }, 200);
            } else {
                $("nav#yooshinanav").toggleClass("in");
            }

            if ($('nav#yooshinanav').hasClass('active')) {
                localStorage.setItem('toranjmenu', 'active');
            } else {
                localStorage.setItem('toranjmenu', '');
            }

        });
    }
});