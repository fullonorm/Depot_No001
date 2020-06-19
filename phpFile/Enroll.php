<?php
include 'Connect.php';

$EMAIL = $_POST['EMAIL'];
$PASSWORD = $_POST['PASSWORD'];
$REPASSWORD = $_POST['REPASSWORD'];
$NICKNAME = $_POST['NICKNAME'];
      
mysqli_select_db($con,"userinfo");      
$checksql = "SELECT * from UserLogin where EMAIL = '$EMAIL'";
$checkres = $con->query($checksql);

if($checkres->num_rows >=1){
  echo "Exist ID";
  exit;
}

if($PASSWORD != $REPASSWORD){
  echo 'pwd';
  exit;
}

$check_email = filter_var($EMAIL,FILTER_VALIDATE_EMAIL);
if(!$check_email) {
echo "empty";
exit;
}

$secure_pwd = password_hash($PASSWORD,PASSWORD_BCRYPT,['cost'=>12]);

$sql = "insert into UserLogin(EMAIL,PASSWORD) values ('$EMAIL','$secure_pwd')";
      
$result = mysqli_query($con, $sql);
if ($result) {
echo ("Enroll");
} else {
echo 'Not Enroll';
}

$UpdateIPSql = "update UserLogin set IP = '".$_SERVER['REMOTE_ADDR']."' where EMAIL = '$EMAIL'";
mysqli_query($con,$UpdateIPSql);
  
?>

