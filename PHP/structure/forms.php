<?php
function login() {
	return '
		<div id="login" class="form login">
			<form autocomplete="on" action="../functions/login.php" method="Post">
				<label><input type="text" name="username" placeholder="Username"></label>
				<label><input type="password" name="password" placeholder="Password"></label>
				<label><input type="password" name="password" placeholder="Repeat Password"></label>
				<input type="submit" value="Login">
			</form>
		</div>
	';
}

function register() {
	return '
		<div id="register" class="form register">
			<form autocomplete="on" action="../functions/register.php" method="Post">
				<label><input type="text" name="username" placeholder="Username"></label>
				<label><input type="password" name="password" placeholder="Password"></label>
				<label><input type="password" name="password" placeholder="Repeat Password"></label>
				<label><input type="email" name="email" placeholder="Email"></label>
				<label><input type="email" name="email" placeholder="Repeat Email"></label>
				<input type="submit" value="Start Blogging!">
			</form>
		</div>
	';
}

function writePost() {
	return '
		<div id="writePost" class="form writePost">
			<form autocomplete="off" action="../functions/register.php" method="Post">
				<label><input type="text" name="title" placeholder="Post Title"></label>
				<label><textarea name="content"></textarea>
				<input type="submit" value="Save">
			</form>
		</div>
	';
}
?>