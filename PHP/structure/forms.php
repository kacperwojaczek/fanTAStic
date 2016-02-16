<?php
function login() {
	return '
		<div id="login" class="form login">
			<form autocomplete="on" action="../functions/loginAction.php" method="Post">
				<label><input type="text" name="username" placeholder="Username" autofocus></label>
				<label><input type="password" name="password" placeholder="Password"></label>
				<input type="submit" value="Login">
			</form>
		</div>
	';
}

function register() {
	return '
		<div id="register" class="form register">
			<form autocomplete="on" action="../functions/registerAction.php" method="Post">
				<label><input type="text" name="username" placeholder="Username" autofocus></label>
				<label><input type="password" name="password" placeholder="Password"></label>
				<label><input type="email" name="email" placeholder="Email"></label>
				<input type="submit" value="Start Blogging!">
			</form>
		</div>
	';
}

function writePost() {
	return '
		<div id="writePost" class="form writePost">
			<form autocomplete="off" action="../functions/postAction.php?add" method="Post">
				<label><input type="text" name="title" placeholder="Post Title" autofocus></label>
				<label><textarea name="content"></textarea>
				<input type="submit" value="Save">
			</form>
		</div>
	';
}
?>