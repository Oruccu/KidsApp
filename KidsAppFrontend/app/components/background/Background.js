import { ImageBackground, View } from 'react-native';
import React from 'react';
import styles from './styles';

const Background = ({ children, showOverlay }) => {
  return (
    <ImageBackground
      source={require('@/app/assets/dino.jpeg')}
      resizeMode="cover"
      style={styles.image}
    >
      {showOverlay && <View style={styles.overlay} />}
      {children}

    </ImageBackground>
  );
};

export default Background;
