<?php
function navbar($type = "main") {
	if($type === "main") {
		return '
		<nav>'
			.logo("small", "light").
			'<ul class="right">
				<li><button class="button" onclick="modal(\'login\')">Login</button></li>
				<li><button class="button" onclick="modal(\'register\')">Register</button></li>
			</ul>
		</nav>
		';
	}
	elseif($type === "user") {
		return '

		';
	}
}
?>