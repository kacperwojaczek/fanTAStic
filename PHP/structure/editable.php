<?php
function editable() {
	global $loggedIn;
	global $user;

	if($loggedIn && ($_SESSION["user"] === $user)) {
		return " contenteditable=\"true\" onclick=\"return false;\"";
	}
	else return "";
}
?>