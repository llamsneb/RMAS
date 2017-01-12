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
					
					document.getElementById("sun").value = sqlDate(prevSun);
					prevSun.setDate(prevSun.getDate() + 1);
					document.getElementById("mon").value = sqlDate(prevSun);
					prevSun.setDate(prevSun.getDate() + 1);
					document.getElementById("tues").value = sqlDate(prevSun);
					prevSun.setDate(prevSun.getDate() + 1);
					document.getElementById("wed").value = sqlDate(prevSun);
					prevSun.setDate(prevSun.getDate() + 1);
					document.getElementById("thur").value = sqlDate(prevSun);
					prevSun.setDate(prevSun.getDate() + 1);
					document.getElementById("fri").value = sqlDate(prevSun);
					prevSun.setDate(prevSun.getDate() + 1);
					document.getElementById("sat").value = sqlDate(prevSun);								
					}
			});
});

//  Ajax JavaScript code for the RMAS-reservation-availability.html document

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
    var jqxhr = $.ajax("example.php")
      .success(function () {
          document.getElementById("roomNumber").innerHTML = xhr.responseText;
      });



    //var xhr = new XMLHttpRequest();

	//// Register the embedded handler function
	//xhr.onreadystatechange = function () {
	//    if (xhr.readyState == 4 && xhr.status == 200) {
	//	    document.getElementById("roomNumber").innerHTML = xhr.responseText;
	//    }
		
	//}
	//xhr.open("GET", "getRooms.php?roomType=" + roomType);
	//xhr.send(null);
}
