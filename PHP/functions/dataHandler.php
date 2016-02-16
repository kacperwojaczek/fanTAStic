<?php
function dataSend($url, $json) {
	$ch = curl_init();

	$jsonData = json_encode($json);

    curl_setopt($ch, CURLOPT_URL, $url);
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $jsonData);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
	curl_setopt($ch, CURLOPT_HTTPHEADER, array(
		'Content-Type: application/json',
		'Content-Length: ' . strlen($jsonData))
	);  

	curl_exec($ch);
	$result = curl_getinfo($ch);

	curl_close($ch);

	return $result;
}

function dataGet($url) {
	$ch = curl_init();

	curl_setopt($ch, CURLOPT_URL, $url);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);

	$result = curl_exec($ch);
	curl_getinfo($ch);

	curl_close($ch);

	return json_decode($result, 1);
}

function dataPatch($url, $json) {
	$ch = curl_init();

	$jsonData = json_encode($json);

	curl_setopt($ch, CURLOPT_URL, $url);
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "PATCh");
	curl_setopt($ch, CURLOPT_POSTFIELDS, $jsonData);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
	curl_setopt($ch, CURLOPT_HTTPHEADER, array(
		'Content-Type: application/json',
		'Content-Length: ' . strlen($jsonData))
	);  

	curl_exec($ch);
	$result = curl_getinfo($ch);

	curl_close($ch);

	return $result;
}

function sharedPostSend($url, $json) {


	$json = array(
		"Login" => $_POST['username'],
		"PostId" => $_POST['PostId'] #Nie znalazłem... :<<<
	);
	
	
}
?>
