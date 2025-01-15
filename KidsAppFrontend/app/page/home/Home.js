import { View, Text, SafeAreaView } from 'react-native'
import React from 'react'
import Background from '@/app/components/background'
import styles from './styles'
import Banner from '@/app/components/Banner'
import ListenToStory from '@/app/components/homeComponent/ListenToStory'
import { useNavigation } from '@react-navigation/native';
import { getAudioBookById } from '@/app/services/api'; 
import { playAudioFromUrl } from '@/app/utils/playAudioFromUrl'; 


const Home = () => {
  const navigation = useNavigation();

  async function ListenStory() {
    try {
      const audioBook = await getAudioBookById(1);
      console.log('audioBook:', audioBook);
      if (audioBook?.audioFileUrl) {
        console.log(audioBook.audioFileUrl);
        await playAudioFromUrl(audioBook.audioFileUrl);
      }
    } catch (error) {
      console.error(error);
    }
  }

  return (
    <Background showOverlay={true}>
      <SafeAreaView style={styles.container}>
        <Banner bannerTitle={85} />
        <View style={styles.storyContainer}>
          <ListenToStory Title={"The Little Prince"} onPress={ListenStory} url={require(`@/app/assets/icon/prince.png`)}/>
          <ListenToStory Title={"Snow White"} onPress={ListenStory} url={require(`@/app/assets/icon/snowWhite.png`)}/>
        </View>
      </SafeAreaView>
    </Background>
  )
}

export default Home