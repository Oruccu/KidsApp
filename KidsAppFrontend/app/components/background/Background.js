import { ImageBackground } from 'react-native';
import React from 'react';
import styles from './styles';

const Background = ({ children }) => {
  return (
    <ImageBackground 
      source={require('@/app/assets/dino.jpeg')} 
      resizeMode="cover"     
      style={styles.image}
    >
      {children}
    </ImageBackground>
  );
};

export default Background;
