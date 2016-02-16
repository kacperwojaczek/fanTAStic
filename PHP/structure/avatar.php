<?php
function avatar($type ="", $userUrl="", $user="") {
	$avatar ='';

	$class = ' class="avatar';
	if($type !== "") $class = ' class="avatar '.$type;
	$class .= '"';

	$href = ' href="/profile"';
	if($userUrl !== "") $href = ' href="/profile?'. $userUrl .'"';

	$title = ' title="'. $user .'"';

	if($type === "nav") {
		$avatar .= '<ul><li class="button avatar-wrapper"><a'. $title.$href .'>'. '<div '. $class .'></div>hello, '. $user .'</a></li></ul>';
	}
	elseif($type === "small") {
		$avatar .= '<a class="avatar-wrapper"'. $title.$href .'>'. '<div '. $class .'></div>'. $user .'</a>';		
	}
	else {
		$avatar = '<a'. $class.$title.$href .'>'. $user .'</a>';
	}

	return $avatar;
}
?>