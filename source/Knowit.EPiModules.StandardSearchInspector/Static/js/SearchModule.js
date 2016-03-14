var SearchModuleHandler = (function () {
    var searchInput = $(".js-searchmodule-input");
    var resultContainer = $(".js-searchmodule-result");
    var timer = null;
    var loadingResponse = "<div class='loader-container'><img class='loader' src='/modules/Knowit.EPiModules.StandardSearchInspector/Static/gfx/ajax-loader-large.gif'></div>";

    function bindSearchInput() {
        $(searchInput).on("input", function (event) {
            clearTimeout(timer);
            timer = setTimeout(function () {
                getSearchResult($(event.currentTarget).val());
            }, 500);
        });
    }

    function getSearchResult(val) {

        if (val.length <= 0) {
            resultContainer.html("");
            return;
        }

        resultContainer.html(loadingResponse);
        $.ajax({
            url: "//" + window.location.host + window.location.pathname + "Search?query=" + val,
            success: function (e) {
                resultContainer.html(e);
                bindPaginationClick();
            },
            error: function (e) {
                console.log(e.status);
            }
        });
    }

    function bindPaginationClick() {
        var paginationLink = $(".js-pagination-link");
        paginationLink.off("click");
        paginationLink.on("click", function (e) {
            e.preventDefault();
            searchPaginated($(this));
        });
    }

    function searchPaginated(clickedItem) {
        var value = searchInput.val();
        var page = clickedItem.attr("data-page");

        resultContainer.html(loadingResponse);

        $.ajax({
            url: "//" + window.location.host + window.location.pathname + "Search?query=" + value + "&page=" + page,
            dataType: "HTML",
            method: "POST",
            success: function (data) {
                resultContainer.html(data);
                bindPaginationClick();
            },
            error: function (e) {
                resultContainer.html(errorMsg + e.status);
            }
        });
    }

    return {
        init: function () {
            if (searchInput.length <= 0) {
                return;
            }

            bindSearchInput();
        }
    };
})();

SearchModuleHandler.init();

var IframeLoader = (function () {
    var iframeContainer = $(".js-iframe-container");

    function loadIframe() {
        var url = iframeContainer.attr("data-iframeurl");
        var iframe = '<iframe class="reindex-iframe" src="' + url + '"></iframe>';
        iframeContainer.html(iframe);
    }

    return {
        init: function () {
            if (iframeContainer.length <= 0) {
                return;
            }

            loadIframe();
        }
    };
})();
IframeLoader.init();