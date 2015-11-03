<?php
function head($title, $subtitle) {
	global $site;
	?>
		<head>
			<title><?php echo $site ?><?php if($title) echo " | ".$title ?><?php if($subtitle) echo " | ".$subtitle ?></title>

			<!-- meta tags -->
			<meta charset="utf-8" />

			<!-- favicons -->

			<!-- css -->

			<!-- js -->
		</head>
	<?php
}
?>