<?php
function dataSend($url, $jsonData) {
	$jsonDataEncoded = json_encode($jsonData);

	$ch =  curl_init($url);

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $jsonDataEncoded);
	curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json'));

	//$result = curl_exec($ch);

	//curl_close($ch);

	//return $result;
}

function dataGet($url2) {
  	$ch2 = curl_init($url2);

	curl_setopt_array($ch2, array(
		CURLOPT_URL = $url2
	));

	//$answer = curl_exec($ch2);

	//curl_close($ch2);
	
	//return $answer;
}
?>
