$.fn.pageMe = function (opts) {
    var $this = this,
        defaults = {
            perPage: 9,
            showPrevNext: false,
            numbersPerPage: 10,
            hidePageNumbers: false
        },
        settings = $.extend(defaults, opts);

    var listElement = $this;
    var perPage = settings.perPage;
    var children = listElement.children();
    var pager = $('.pagination');

    if (typeof settings.childSelector != "undefined") {
        children = listElement.find(settings.childSelector);
    }

    if (typeof settings.pagerSelector != "undefined") {
        pager = $(settings.pagerSelector);
    }

    var numItems = children.length;
    var numPages = Math.ceil(numItems / perPage);

    pager.data("curr", 0);

    if (settings.showPrevNext) {
        $('<li><a href="#" class="prev_link">«</a></li>').appendTo(pager);
    }

    var curr = 0;
    while (numPages > curr && (settings.hidePageNumbers == false)) {
        $('<li><a href="#" class="page_link">' + (curr + 1) + '</a></li>').appendTo(pager);
        curr++;
    }

    if (settings.numbersPerPage > 1) {
        $('.page_link').hide();
        $('.page_link').slice(pager.data("curr"), settings.numbersPerPage).show();
    }

    if (settings.showPrevNext) {
        $('<li><a href="#" class="next_link">»</a></li>').appendTo(pager);
    }

    pager.find('.page_link:first').addClass('active');
    pager.find('.prev_link').hide();
    if (numPages <= 1) {
        pager.find('.next_link').hide();
    }
    pager.children().eq(1).addClass("active");

    children.hide();
    children.slice(0, perPage).show();

    goTo();

    pager.find('li .page_link').click(function () {
        var clickedPage = $(this).html().valueOf() - 1;
        document.getElementById("TXT_Pagina").value = clickedPage;
        goTo();
        return false;
    });
    pager.find('li .prev_link').click(function () {
        previous();
        return false;
    });
    pager.find('li .next_link').click(function () {
        next();
        return false;
    });

    function previous() {
        var goToPage = parseInt(pager.data("curr")) - 1;
        document.getElementById("TXT_Pagina").value = goToPage;
        goTo();
    }

    function next() {
        goToPage = parseInt(pager.data("curr")) + 1;
        document.getElementById("TXT_Pagina").value = goToPage;
        goTo();
    }

    function goTo() {

        var pagina = document.getElementById("TXT_Pagina").value;

        var startAt = pagina * perPage, endOn = parseInt(startAt) + parseInt(perPage);

        children.css('display', 'none').slice(startAt, endOn).show();

        if (pagina >= 1) {
            pager.find('.prev_link').show();
        }
        else {
            pager.find('.prev_link').hide();
        }

        if (pagina < (numPages - settings.numbersPerPage)) {
            pager.find('.next_link').show();
        }
        else {
            pager.find('.next_link').hide();
        }

        pager.data("curr", parseInt(pagina));

        if (settings.numbersPerPage > 1) {
            $('.page_link').hide();

            var mitad = 0;

            if (parseInt(pagina) + 1 > 5) {

                mitad = parseInt(pagina) - 5;

                $('.page_link').slice(mitad, parseInt(settings.numbersPerPage) + parseInt(mitad)).show();

            }
            else {
                mitad = parseInt(pagina);

                $('.page_link').slice(0, parseInt(settings.numbersPerPage)).show();
            }
        }

        pager.children().removeClass("active");
        pager.children().eq(parseInt(pagina) + 1).addClass("active");

    }
};