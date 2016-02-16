<?php
function navbar() {
	global $loggedIn;

	$page = basename($_SERVER['PHP_SELF']);
	
	$navbar = '';

	if($loggedIn) {
		$navbar .= '<nav>'.
			avatar("nav", $_SESSION["user"], $_SESSION["user"]).
			'<ul class="right">';

			if($page === "profile.php" || $page === "home.php") $navbar .= '<li><button class="button color" onclick="save(\''. $page .'\')">Save changes</button></li>';


		$navbar .= '<li><a class="button" href="/functions/logoutAction">Logout</a></li>
			</ul>
		</nav>';
	}
	else {
		$navbar .= '<nav>'.
			logo("small", "light").
			'<ul class="right">
				<li><button class="button" onclick="modal(\'login\')">Login</button></li>
				<li><button class="button" onclick="modal(\'register\')">Register</button></li>
			</ul>
		</nav>';
	}

	return $navbar;
}
?>
