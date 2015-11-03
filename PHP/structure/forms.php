<?php
function login() {
	?>
		<form action="../functions/login.php" method="Post">
			<label><input type="text" name="username" placeholder="Username"></label>
			<label><input type="password" name="password" placeholder="Password"></label>
			<input type="submit" value="Login">
		</form>
	<?php
}

function register() {
	?>
		<form action="../functions/register.php" method="Post">
			<label><input type="text" name="username" placeholder="Username"></label>
			<label><input type="password" name="password" placeholder="Password"></label>
			<label><input type="email" name="email" placeholder="Email"></label>
			<input type="submit" value="Start Blogging!">
		</form>
	<?php
}
?>