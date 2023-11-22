var CalendarModule = (function () {
    document.addEventListener('DOMContentLoaded', function () {
        var calendarEl = document.getElementById('calendar');
        calendar = new FullCalendar.Calendar(calendarEl, {
            initialView: 'dayGridMonth',
            headerToolbar: {
                left: 'prev,next today',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek,timeGridDay'
            },
            //eventColor: 'red'
            events:
            {
                url: '/Calendar/GetEventsFullCalendar',
                method: 'POST',
                extraParams: function () {
                    return { roomNumber: $('#RoomNumber').val() }
                },
                failure: function () {
                    alert('there was an error while fetching events!');
                },
                //color: 'yellow',   // a non-ajax option
                //textColor: 'black' // a non-ajax option
            }
        });
        calendar.render();        

        // Event Handlers:
        $('#RoomNumber').change(
            function () {
                if ($('#RoomNumber').val() != null && $('#RoomNumber').val().length != 0) {
                    calendar.refetchEvents();
                }
                else {
                    calendar.removeAllEvents();
                }
            }
        ); 
    });       
})();