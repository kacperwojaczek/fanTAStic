<?php
function navbar() {
	global $loggedIn;

	$page = basename($_SERVER['PHP_SELF']);
	
	$navbar = '<nav>';

	if($loggedIn) {
		$navbar .= ''.
			avatar("nav", $_SESSION["user"], $_SESSION["user"]).
			'<ul class="right">
				<li><form style="width: 200px;" action="../functions/allFriendsAction.php" method="Post">
					<input style="display: inline-block; width: 200px;" style="display:inline-block" type="submit" value="Show All Friends"></form>
				</li>
				<li><form style="width: 510px;" action="../functions/searchAction.php" method="Post">
					<label style="display: inline-block;"><input type="text" name="query" placeholder="Query" autofocus></label>
					<input style="display: inline-block; width: 100px;" style="display:inline-block" type="submit" value="Search">
				</form></li>';

			if($page === "profile.php" || $page === "home.php") $navbar .= '<li><button class="button color" onclick="save(\''. $page .'\')">Save changes</button></li>';


		$navbar .= '<li><a class="button" href="/functions/logoutAction">Logout</a></li>
			</ul>
		</nav>';
	}
	else {
		$navbar .= ''.
			logo("small", "light").
			'<ul class="right">
				<li><form style="width: 510px;" action="../functions/searchAction.php" method="Post">
					<label style="display: inline-block;"><input type="text" name="query" placeholder="Query" autofocus></label>
					<input style="display: inline-block; width: 100px;" style="display:inline-block" type="submit" value="Search">
				</form></li>
				<li><button class="button" onclick="modal(\'login\')">Login</button></li>
				<li><button class="button" onclick="modal(\'register\')">Register</button></li>
			</ul>
		</nav>';
	}

	return $navbar;
}
?>
