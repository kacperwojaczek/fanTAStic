<?php
function head($title = "", $subtitle = "") {
	global $site;
	?>
		<head>
			<!-- title -->
			<title><?php echo $site ?><?php if($title !== "") echo " | ".$title ?><?php if($subtitle !== "") echo " | ".$subtitle ?></title>

			<!-- meta tags -->
			<meta charset="utf-8" />

			<!-- favicons -->
			<link rel="apple-touch-icon" sizes="57x57" href="/styles/favicons/apple-touch-icon-57x57.png">
			<link rel="apple-touch-icon" sizes="60x60" href="/styles/favicons/apple-touch-icon-60x60.png">
			<link rel="apple-touch-icon" sizes="72x72" href="/styles/favicons/apple-touch-icon-72x72.png">
			<link rel="apple-touch-icon" sizes="76x76" href="/styles/favicons/apple-touch-icon-76x76.png">
			<link rel="apple-touch-icon" sizes="114x114" href="/styles/favicons/apple-touch-icon-114x114.png">
			<link rel="apple-touch-icon" sizes="120x120" href="/styles/favicons/apple-touch-icon-120x120.png">
			<link rel="apple-touch-icon" sizes="144x144" href="/styles/favicons/apple-touch-icon-144x144.png">
			<link rel="apple-touch-icon" sizes="152x152" href="/styles/favicons/apple-touch-icon-152x152.png">
			<link rel="apple-touch-icon" sizes="180x180" href="/styles/favicons/apple-touch-icon-180x180.png">
			<link rel="icon" type="image/png" href="/styles/favicons/favicon-32x32.png" sizes="32x32">
			<link rel="icon" type="image/png" href="/styles/favicons/favicon-194x194.png" sizes="194x194">
			<link rel="icon" type="image/png" href="/styles/favicons/favicon-96x96.png" sizes="96x96">
			<link rel="icon" type="image/png" href="/styles/favicons/android-chrome-192x192.png" sizes="192x192">
			<link rel="icon" type="image/png" href="/styles/favicons/favicon-16x16.png" sizes="16x16">
			<link rel="manifest" href="/manifest.json">
			<link rel="mask-icon" href="/styles/favicons/safari-pinned-tab.svg" color="#f85145">
			<meta name="apple-mobile-web-app-title" content="ipsu.me">
			<meta name="application-name" content="ipsu.me">
			<meta name="msapplication-TileColor" content="#f2f2f2">
			<meta name="msapplication-TileImage" content="/styles/favicons/mstile-144x144.png">
			<meta name="theme-color" content="#f85145">

			<!-- css -->
			<link rel="stylesheet" href="/styles/css/main.css">

			<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Wire+One&subset=latin,latin-ext">
			<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Unica+One&subset=latin,latin-ext">
			<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Vollkorn:400,400italic,700italic,700&subset=latin,latin-ext">
		</head>
	<?php
}
?>