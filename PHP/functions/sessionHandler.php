<?php
	session_start();

	$loggedIn = false;
	if(isset($_SESSION['id'])) $loggedIn = true;
?>