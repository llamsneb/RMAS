var DateModule = (function () {

    // Format date for SQL insert.
    var sqlDate = function (date) {
        var year = date.getFullYear();
        var month = date.getMonth() + 1;
        var day = date.getDate();

        if (month.length < 2)
            month = '0' + month;
        if (day.length < 2)
            day = '0' + day;

        return [year, month, day].join('-');
    }     

    function addHiddenDate(dateString) {
        var itemCount = $("input[name='Dates']").length;
        var dat = $("<input type='hidden' asp-for='Dates' name='Dates' id='Dates[" + itemCount + "]' /> ").val(dateString);
        $("form#Reserve").append(dat);
    }    

    $(document).ready(function () {
        let selectedDates = [];
        //Persist dates after submit
        //let hiddenDates = $('input[name="Dates"]');
        //if (hiddenDates.length > 0) {
        //    for (let i = 0; i < hiddenDates.length; i++) 
        //    {
        //        selectedDates.push(hiddenDates[i].value);
        //    };
        //}

        $("#datepicker").datepicker({
            minDate: 0,
            // Called on date select
            onSelect: function (dateText, obj) {
                let dt = sqlDate(new Date(dateText));
                if (selectedDates.includes(dt)) {
                    selectedDates = selectedDates.filter(date => date !== dt);
                    $('input[name="Dates"][value=' + dt +']').remove();
                    $("div#datepicker td.date-selected.ui-datepicker-current-day").removeClass("date-selected");
                } else {
                    addHiddenDate(dt)
                    selectedDates.push(dt);
                }
            },
            // Called for each date in calendar
            beforeShowDay: function (date) {
                for (let i = 0; i < selectedDates.length; i++) {
                    if (sqlDate(date) == selectedDates[i]) {
                        return [true, "date-selected"];
                    }
                }
                return [true,''];
            },
            // Called before calendar loads
            onUpdateDatepicker: function (obj) {
                $("div#datepicker td .ui-state-active").removeClass("ui-state-active");               
            }            
        });        
    });        

    // Event Handlers:  
    $("#search-results").on("click", "#select-all",
        function () {
            $("input[type=checkbox]").prop('checked', $(this).prop('checked'));
        });

    $('#search-results').on("change", 'input.form-check-input', function () {
        $('#cancelButton').prop('disabled', $('td input.form-check-input').filter(':checked').length < 1);
    });    

    $('#search-results').on("click", "#previousButton, #nextButton",function () {    
        $.ajax({
            type: "POST",
            url: $(this).data('url'),
            data: {
                'dates': $(this).data('dates'),
                'roomNumber': $(this).data('roomnumber'),
                'beginTime': $(this).data('begintime'),
                'endTime': $(this).data('endtime'),
                'eventName': $(this).data('eventname'),
                'eventDate': $(this).data('eventdate'),
                'pageNumber': $(this).data('pagenumber') //pass as string
            }
        })
        .success(function (result, status) {
            $("#search-results").html(result);
        })
        .error(function (xhr, textStatus, errorThrown) {
            alert('there was an error while fetching events!');
        });
    });


    // Expose Methods:
    return {
        sqlDate: sqlDate,
    };    
})();