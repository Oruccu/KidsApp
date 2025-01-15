import { View, Text, Image } from 'react-native'
import React from 'react'
import styles from './styles'
const Banner = ({ bannerTitle}) => {
  return (
      <View style={styles.container}>
        <View style={styles.imageContainer}>
        <Image source={require(`@/app/assets/icon/girlIcon.png`)} style={styles.image} />
        </View>
        <View style={styles.innerContainer}>
          <Text style={styles.score}>{bannerTitle}</Text>
        </View>
      </View>
  )
}

export default Banner