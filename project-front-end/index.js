function getUsers() {
	$.ajax({
	    url: 'http://127.0.0.1:3670/api/account',
	    data: {
	
	    },
	    type: "GET",
	    success: function(result) {
		     var test = result.content;
			 
			 debugger;
	    },
            error: function() {		
		debugger;
	    },
	    beforeSend: function() {
		debugger;
 	    }
	});
}