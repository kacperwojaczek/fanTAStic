<?php
	include_once '../config.php';
	include_once 'dataHandler.php';

	$url = $urlBackend.'/login';
	$json = array(
		"Login" => $_POST['username'],
		"Password" => $_POST['password']
	);
	$result = dataSend($url, $json);

	if($result['http_code'] === 200) {
		session_regenerate_id(true);
    	session_start();
    	$_SESSION['id'] = uniqid();
    	$_SESSION['user'] = $_POST['username'];
		session_write_close();
		header('Location: /home?'. $_POST['username']);
		exit;
	} else {
		header('Location: /?error'. $result['http_code'] .'#login');
    	exit;
	}
?>