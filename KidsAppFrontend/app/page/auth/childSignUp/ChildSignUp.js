import React, { useState } from 'react';
import { View, Alert, ActivityIndicator, SafeAreaView, Text } from 'react-native';
import { Formik } from 'formik';
import * as Yup from 'yup';
import Background from '@/app/components/background';
import Button from '@/app/components/button';
import Input from '@/app/components/input';
import AuthTextButton from '@/app/components/authTextButton';
import styles from './styles';

import { registerChild } from '@/app/services/api';
import { useNavigation } from '@react-navigation/native';

const ChildSignUp = () => {
  const navigation = useNavigation();
  const [isRegistering, setIsRegistering] = useState(false);

  // Formik validation schema
  const ChildSignUpSchema = Yup.object().shape({
    username: Yup.string()
      .min(3, 'Username must be at least 3 characters')
      .required('Please enter your username'),
    childEmail: Yup.string()
      .email('Invalid email')
      .required('Please enter your email'),
    parentName: Yup.string()
      .min(3, 'Parent name must be at least 3 characters')
      .required('Please enter parent name'),
    childPassword: Yup.string()
      .min(6, 'Password must be at least 6 characters')
      .required('Please enter your password'),
    confirmChildPassword: Yup.string()
      .oneOf([Yup.ref('childPassword'), null], 'Passwords do not match')
      .required('Please confirm your password'),
  });

  // Kayıt fonksiyonu
  const handleChildSignUp = async (values, { resetForm, setSubmitting }) => {
    try {
      setIsRegistering(true);

      const response = await registerChild(
        values.childEmail,
        values.childPassword,
        values.username,
        values.parentName
      );
      console.log('API Response:', response.data);
      
      if (!response.IsSucced) {
        console.log('Error', response.Message);
      }
  
      // Başarılı kayıt
      Alert.alert('Success', 'Registration successful!');
      resetForm();
      navigation.navigate('SignIn');
    } catch (error) {
      Alert.alert('Error', error.message || 'An error occurred.');
      console.log(error);
    } finally {
      setSubmitting(false);
      setIsRegistering(false);

    }
  };

  const goSignIn = () => {
    navigation.navigate('SignIn');
  };

  return (
    <Background>
      <SafeAreaView style={styles.container}>
        <Formik
          initialValues={{
            username: '',
            childEmail: '',
            parentName: '',
            childPassword: '',
            confirmChildPassword: '',
          }}
          validationSchema={ChildSignUpSchema}
          onSubmit={handleChildSignUp}
        >
          {({
            handleChange,
            handleBlur,
            handleSubmit,
            values,
            errors,
            touched,
            isSubmitting,
          }) => (
            <View>
              <Input
                placeholder="Child Username"
                onChangeText={handleChange('username')}
                onBlur={handleBlur('username')}
                value={values.username}
                iconName="user"
              />
              {errors.username && touched.username && (
                <Text style={styles.errorText}>{errors.username}</Text>
              )}

              <Input
                placeholder="Child E-Mail"
                onChangeText={handleChange('childEmail')}
                onBlur={handleBlur('childEmail')}
                value={values.childEmail}
                iconName="mail"
                keyboardType="email-address"
                autoCapitalize="none"
              />
              {errors.childEmail && touched.childEmail && (
                <Text style={styles.errorText}>{errors.childEmail}</Text>
              )}

              <Input
                placeholder="Parent Name"
                onChangeText={handleChange('parentName')}
                onBlur={handleBlur('parentName')}
                value={values.parentName}
                iconName="user"
                autoCapitalize="words"
              />
              {errors.parentName && touched.parentName && (
                <Text style={styles.errorText}>{errors.parentName}</Text>
              )}

              <Input
                placeholder="Child Password"
                onChangeText={handleChange('childPassword')}
                onBlur={handleBlur('childPassword')}
                value={values.childPassword}
                iconName="key"
                secureTextEntry
              />
              {errors.childPassword && touched.childPassword && (
                <Text style={styles.errorText}>{errors.childPassword}</Text>
              )}

              <Input
                placeholder="Confirm Child Password"
                onChangeText={handleChange('confirmChildPassword')}
                onBlur={handleBlur('confirmChildPassword')}
                value={values.confirmChildPassword}
                iconName="key"
                secureTextEntry
              />
              {errors.confirmChildPassword && touched.confirmChildPassword && (
                <Text style={styles.errorText}>{errors.confirmChildPassword}</Text>
              )}

              {isRegistering || isSubmitting ? (
                <ActivityIndicator size="large" color="#0000ff" />
              ) : (
                <Button onPress={handleSubmit} ButtonName="SIGN IN" />
              )}

              <AuthTextButton
                text="I have an account! Sign In"
                onPress={goSignIn}
              />
            </View>
          )}
        </Formik>
      </SafeAreaView>
    </Background>
  );
};

export default ChildSignUp;
