import { View, Text, SafeAreaView } from 'react-native'
import React from 'react'
import Background from '@/app/components/background'
import styles from './styles'
import Banner from '@/app/components/homeComponent/Banner'
import ListenToStory from '@/app/components/homeComponent/ListenToStory'
import { useNavigation } from '@react-navigation/native';
const Home = () => {
  const navigation = useNavigation();
  
  function ListenStory() {
    navigation.navigate('Story');
  }
  return (
    <Background showOverlay={true}>
      <SafeAreaView style={styles.container}>
        <Banner Score={85} />
        <View style={styles.storyContainer}>
          <ListenToStory Title={"The Little Prince"} onPress={ListenStory} url={require(`@/app/assets/icon/prince.png`)}/>
          <ListenToStory Title={"Snow White"} onPress={ListenStory} url={require(`@/app/assets/icon/snowWhite.png`)}/>
        </View>
      </SafeAreaView>
    </Background>
  )
}

export default Home