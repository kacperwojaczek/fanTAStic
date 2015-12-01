<?php
function js() {
	$result = '
	<script>
		var scrolled;

		function modal(type) {
			var modal;

			if(type == "register") modal = "'. trim(str_replace("\"", "\\\"", preg_replace('/\s+/', ' ', registerModal() ))) .'";
			if(type == "login") modal = "'. trim(str_replace("\"", "\\\"", preg_replace('/\s+/', ' ', loginModal() ))) .'";

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