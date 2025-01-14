import { View, Text, Image } from 'react-native'
import React from 'react'
import styles from './styles'
const Banner = ({ Score}) => {
  return (
      <View style={styles.container}>
        <View style={styles.imageContainer}>
        <Image source={require(`@/app/assets/icon/girlIcon.png`)} style={styles.image} />
        </View>
        <View style={styles.innerContainer}>
          <Text style={styles.score}>Score {Score}</Text>
        </View>
      </View>
  )
}

export default Banner