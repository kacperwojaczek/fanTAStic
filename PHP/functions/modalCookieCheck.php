<?php
function modalCookieCheck($cookieName = "") {
	if($_COOKIE[$cookieName] === "hidden") {
		return false;
	}
}
?>