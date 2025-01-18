import React from 'react';
import { Text, Image, TouchableOpacity } from 'react-native';
import { useSelector } from 'react-redux';
import styles from './styles';


const ContinueToListen = ({ onPress }) => {
  const currentMode = useSelector((state) => state.mode.currentMode);

  const containerStyle = [
    styles.container,
    {
      backgroundColor: currentMode === 'Boy' ? styles.colors.BoyColors.Primary : styles.colors.GirlColors.Primary,
    },
  ];

  return (
    <TouchableOpacity onPress={onPress} style={containerStyle}>
      <Text style={styles.text}>Listening to Your Story</Text>
      <Image source={require('@/app/assets/listen.png')} style={styles.image} />
    </TouchableOpacity>
  );
};

export default ContinueToListen;