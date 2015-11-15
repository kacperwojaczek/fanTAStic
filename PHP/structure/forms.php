<?php
function login() {
	?>
		<div class="form login">
			<h2>Login</h2>
			<form autocomplete="on" action="../functions/login.php" method="Post">
				<label><input type="text" name="username" placeholder="Username"></label>
				<label><input type="password" name="password" placeholder="Password"></label>
				<input type="submit" value="Login">
			</form>
		</div>
	<?php
}

function register() {
	?>
		<div class="form register">
			<h2>Register</h2>
			<form autocomplete="on" action="../functions/register.php" method="Post">
				<label><input type="text" name="username" placeholder="Username"></label>
				<label><input type="password" name="password" placeholder="Password"></label>
				<label><input type="email" name="email" placeholder="Email"></label>
				<input type="submit" value="Start Blogging!">
			</form>
		</div>
	<?php
}
?>