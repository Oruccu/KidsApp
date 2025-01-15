// src/app/components/Banner.js
import { View, Text, Image } from 'react-native';
import React from 'react';
import styles from './styles';
import useTheme from '@/app/hooks/useTheme';
import { useSelector } from 'react-redux';

const Banner = ({ bannerTitle }) => {
  const themeColors = useTheme();
  const currentMode = useSelector((state) => state.mode.currentMode);

  const iconSource =
    currentMode === 'boy'
      ? require('@/app/assets/icon/boyIcon.png')
      : require('@/app/assets/icon/girlIcon.png');

  return (
    <View style={styles.bannerContainer}>
      <View style={[styles.imageContainer, { backgroundColor: themeColors.Secondary }]}>
        <Image source={iconSource} style={styles.image} />
      </View>
      <View style={[styles.innerContainer, { backgroundColor: themeColors.Primary }]}>
        <Text style={[styles.score, { color: themeColors.Secondary }]}>{bannerTitle}</Text>
      </View>
    </View>
  );
};

export default Banner;
