import React, { useState } from 'react';
import { View, SafeAreaView, Alert } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { useDispatch } from 'react-redux';
import AsyncStorage from '@react-native-async-storage/async-storage';

import Background from '@/app/components/background';
import Button from '@/app/components/button';
import Input from '@/app/components/input';
import AuthTextButton from '@/app/components/authTextButton';
import styles from './styles';
import { login } from '@/app/services/api';
import { setUser } from '@/app/store/userSlice';

const SignIn = () => {
  const navigation = useNavigation();
  const dispatch = useDispatch();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  async function handleSignIn() {
    try {
      console.log('Sign In button pressed.');
      const userData = await login(email, password);
      console.log('Login successful:', userData);
      
      dispatch(setUser(userData));

      navigation.navigate('Main'); 
    } catch (error) {
      navigation.navigate('Main'); 
      console.error('Login error:', error.message);
    }
  }

  function forgotPassword() {
    console.log('Forgot password tapped.');
    navigation.navigate('ResetPassword');
  }

  function goParentSignUp() {
    console.log('Navigating to ParentSignUp screen...');
    navigation.navigate('ChildSignUp');
  }

  return (
    <Background>
      <SafeAreaView style={styles.container}>
        <Input
          placeholder="E-Mail"
          onChangeText={setEmail}
          value={email}
          iconName="mail"
        />
        <Input
          placeholder="Password"
          onChangeText={setPassword}
          value={password}
          iconName="key"
          secureTextEntry
        />
        <View style={styles.forgotPassword}>
          <AuthTextButton
            text="Forgot Password?"
            onPress={forgotPassword}
          />
        </View>
        <Button
          onPress={handleSignIn}
          ButtonName="Sign In"
        />
        <AuthTextButton
          text="Don’t have an account yet? Sign Up"
          onPress={goParentSignUp}
        />
      </SafeAreaView>
    </Background>
  );
};

export default SignIn;
