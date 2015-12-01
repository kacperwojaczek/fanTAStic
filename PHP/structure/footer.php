<?php
function footer() {
	global $site;
	return '
		<footer>
			<nav>
				<ul>
					<li><a href="/team">'. $site .'&copy; 2015 The Team</a></li>
					<li><a href="https://github.com/Gregorein/fanTAStic">Code @ GitHub</a></li>
					<li><a href="/policy">Policy</a></li>
				</ul>
			</nav>
		</footer>
		'. js();
}
?>