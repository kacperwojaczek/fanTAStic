<?php
function js() {
	$result = '
	<script>
		var backend = "http://ipsume2.azurewebsites.net";
		var scrolled;

		function sendData(path, params, method) {
		    method = method || "post"; // Set method to post by default if not specified.

		    var form = document.createElement("form");
		    form.setAttribute("method", method);
		    form.setAttribute("action", path);

		    for(var key in params) {
		        if(params.hasOwnProperty(key)) {
		            var hiddenField = document.createElement("input");
		            hiddenField.setAttribute("type", "hidden");
		            hiddenField.setAttribute("name", key);
		            hiddenField.setAttribute("value", params[key]);

		            form.appendChild(hiddenField);
		         }
		    }

		    document.body.appendChild(form);
		    form.submit();
		}

		function modal(type) {
			var modal;

			if(type == "register") modal = "'. trim(str_replace("\"", "\\\"", preg_replace('/\s+/', ' ', registerModal() ))) .'";
			if(type == "login") modal = "'. trim(str_replace("\"", "\\\"", preg_replace('/\s+/', ' ', loginModal() ))) .'";
			if(type == "save") modal = "'. trim(str_replace("\"", "\\\"", preg_replace('/\s+/', ' ', saveModal() ))) .'";

			document.getElementById("modal-control").className = "";
			document.getElementById("modal-wrapper").className = "";

			document.getElementById("modal-wrapper").innerHTML = modal;
		};

		function modalClose() {
			document.getElementById("modal-control").className = "hidden";

			document.getElementById("modal-wrapper").className = "hide";
			setTimeout(function(){
				document.getElementById("modal-wrapper").className = "hidden";
			}, 1000);

			if(document.cookie !== "modal-launch=true") {
				var today = new Date();
				var expire = new Date();
				expire.setTime(today.getTime() + 3600000*24*30);
				document.cookie = "modal-launch=hidden"+";expires="+expire.toGMTString();
			}

			return false;
		}

		function save(type) {
			if(type == "profile.php") {
				var fn = document.getElementById("firstname").innerHTML;
				var ln = document.getElementById("lastname").innerHTML;
				var b = document.getElementById("bio").innerHTML;

				sendData("/functions/profileAction.php", {Firstname: fn, Lastname: ln, Bio: b});
				
				modal("save");
			}
			else if(type == "home.php") {
				var t = document.getElementById("new-post").getElementsByTagName("H2")[0].innerHTML;
				var c = document.getElementById("new-post").getElementsByTagName("p")[0].innerHTML;

				if((t !== "New Post") || (c !== "Write your story")) {
					sendData("/functions/postAction.php?new", {Title: t, Content: c});

					modal("save");
				}
			}
		}

		document.addEventListener("readystatechange", function() {
			if (document.readyState === "complete") {

				document.addEventListener("scroll", function() {
					var nav = document.getElementsByTagName("nav")[0];
					var logo = nav.getElementsByTagName("a")[0];
					if(document.body.scrollTop >= 50 && scrolled == false) {
						nav.className = "scrolled";
						scrolled = true;
						logo.className = logo.className.replace("light", "dark");
					}
					if(document.body.scrollTop < 50) {
						nav.className = "";
						scrolled = false;
						logo.className = logo.className.replace("dark", "light");
					}
				});
				
			}
		});
	</script>
	';

	return $result;
}
?>