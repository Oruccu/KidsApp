import { View, Text, SafeAreaView } from 'react-native'
import React, { useState } from 'react';
import Background from '@/app/components/background';
import Button from '@/app/components/button';
import Input from '@/app/components/input'
import AuthTextButton from '@/app/components/authTextButton'
import styles from './styles'

const ResetPassword = () => {
  const [parentUsername, setparentUsername] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  function ParentSingUp() {
    console.log("Navigating to Parent Sign Up...");
  }

  function goSingIn() {

  }
  
  return (
    <Background>
      <SafeAreaView style={styles.container}>
        <Input
          placeholder={'Username'}
          onChangeText={setparentUsername}
          value={parentUsername}
          iconName={'user'}
        />
        <Input
          placeholder={'Password'}
          onChangeText={setPassword}
          value={password}
          iconName={'key'}
        />
        <Input
          placeholder={'Password'}
          onChangeText={setConfirmPassword}
          value={confirmPassword}
          iconName={'key'}
        />
        <Button
          onPress={ParentSingUp}
          ButtonName={"Reset Password"}
        />
      </SafeAreaView>

    </Background>
    )
}

export default ResetPassword