<?php
function splashModal() {
	return '
		<div id="modal" class="splash">
			'. logo() .'
			<a class="form-header login" href="#login"><h2>Login</h2></a>
			'. login() .'
			<a class="form-header register" href="#register"><h2>Register</h2></a>
			'. register() .'
			<div id="explore">
				<h2>or simply</h2>
				<div onclick="modalClose()">Browse the posts created by Community</div>
			</div>
		</div>
	';
}

function registerModal() {
	return '
		<div id="modal">
			<div id="close" title="close" onclick="modalClose()">✕</div>
			'. register() .'
		</div>
	';
}

function loginModal() {
	return '
		<div id="modal">
			<div id="close" title="close" onclick="modalClose()">✕</div>
			'. login() .'
		</div>
	';
}

function saveModal() {
	return '
		<div id="modal">
			<div id="close" title="close" onclick="modalClose()">✕</div>
				<p>Changes have been saved.</p>
				<div class="button" onclick="modalClose()>I am okay with that.</div>
			</div>
		</div>
	';
}

?>