// Format date for SQL insert.
function sqlDate(date) {
	var curr_year = date.getFullYear();
	var curr_month = date.getMonth() + 1;
	var curr_date = date.getDate();
	return (curr_year + "-" + curr_month + "-" + curr_date);	
}

// Format date to be shown in textbox.
function boxDate(date) {
	var curr_year = date.getFullYear();
	var curr_month = date.getMonth() + 1;
	var curr_date = date.getDate();
	return (curr_month + "/" + curr_date + "/" + curr_year );	
}

// Display datepicker. Calculate date values for checkboxes.
$(function() {
	$( "#datepicker" ).datepicker({
			onSelect : function(dateText, inst) {
					//document.getElementById("date").value = dateText;
					var prevSun = new Date(dateText);
					var d = prevSun.getDay();
					prevSun.setDate(prevSun.getDate() - d);								
					document.getElementById("date").value = boxDate(prevSun);
					
					document.getElementById("Sun").value = sqlDate(prevSun);
					prevSun.setDate(prevSun.getDate() + 1);
					document.getElementById("Mon").value = sqlDate(prevSun);
					prevSun.setDate(prevSun.getDate() + 1);
					document.getElementById("Tues").value = sqlDate(prevSun);
					prevSun.setDate(prevSun.getDate() + 1);
					document.getElementById("Wed").value = sqlDate(prevSun);
					prevSun.setDate(prevSun.getDate() + 1);
					document.getElementById("Thur").value = sqlDate(prevSun);
					prevSun.setDate(prevSun.getDate() + 1);
					document.getElementById("Fri").value = sqlDate(prevSun);
					prevSun.setDate(prevSun.getDate() + 1);
					document.getElementById("Sat").value = sqlDate(prevSun);								
			}
	});


	//function modifyDatesList() {
	//    var dates = new Array();
	//    for (i = 0; i < ids.length; i++) {
	//        if ($("#" + ids[i].toString() + ".cb").is(':checked')) {
	//            dates.push(ids[i]);
	//        }
	//    }
	//    console.log(dates.toString());
	//    $("#str").val(dates.toString());
	//}



	$('#RoomType').change(
        function () {
            getRooms(this.value)
        }
    );
});

/********************************************************/
// function getRooms
//  parameter: room type
//  action: create the XMLHttpRequest object, register the
//          handler for onreadystatechange, prepare to send
//          the request (with open), and send the request,
//          along with the zip code, to the server
//  includes: the anonymous handler for onreadystatechange, 
//            which is the receiver function, which gets the 
//            response text, and puts them in the document

// Ajax function. Calls php function that populates dropdown of all room numbers for selected RoomType.
function getRooms(roomType) {
    let jqxhr = $.ajax({
        type: "POST",
        url: "/Reserve/GetRooms",
        data: {'roomType': roomType},
        //contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: (function (response) {
            let select = document.getElementById("RoomNumber");

            for (let i = select.options.length; i >= 0; i--) {
                select.remove(i);
            }

            response.forEach(function (num) {
                select.options.add(new Option(num));
            })
        })
    });
}