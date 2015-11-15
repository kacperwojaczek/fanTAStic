<?php
function js() {
	?>
	<script>
		var scrolled;

		function init() {
			<?php if($_COOKIE[$cookieName] === "hidden") { ?>
			document.getElementById("modal-close").addEventListener("click", function() {
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
			});
			<?php } ?>

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

		document.addEventListener('readystatechange', function() {
			if (document.readyState === "complete") {
				init();
			}
		});
	</script>
	<?php
}
?>