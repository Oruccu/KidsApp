import { View, Text, Image, TouchableOpacity } from 'react-native';
import React from 'react';
import { useSelector } from 'react-redux';
import styles from './styles';

const ListenToStory = ({ Title, onPress, url }) => {
  const currentMode = useSelector((state) => state.mode.currentMode);
  const containerStyle = [
    styles.container,
    {
      backgroundColor:
        currentMode === 'Boy' ? styles.colors.BoyColors.Primary : styles.colors.GirlColors.Primary,
    },
  ];

  return (
    <TouchableOpacity onPress={onPress}>
      <View style={containerStyle}>
        <Image source={url} style={styles.image} />
        <Text style={styles.title} numberOfLines={2}>{Title}</Text>
      </View>
    </TouchableOpacity>
  );
};

export default ListenToStory;