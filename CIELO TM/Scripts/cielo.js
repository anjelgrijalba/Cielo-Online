

$(function () {
    var ajaxFormSubmit = function () {
        var $form = $(this);
        var options = {

            url: $form.attr("action"),
            type: $form.attr("method"),
            data:$form.serialize()

        };

        $.ajax(options).done(function (data) {

            var $target = $($form.attr("data-cielo-target"));
            $target.replaceWith(data);
        });

        return false;
    };

    var submitAutoCompleteForm=function(event, ui){
        var $input = $(this);
        $input.val(ui.item.label);

        var $form = $input.parent("form:first");
        $form.submit();

    };

    var createAutoComplete = function () {

        var $input = $(this);

        var options = {
            source: $input.attr("data-cielo-autoComplete"),
            select: submitAutoCompleteForm
        };
        $input.autocomplete(options);
    };

    $("form[data-cielo-ajax='true']").submit(ajaxFormSubmit);
    $("input[data-cielo-autoComplete]").each(createAutoComplete);

});