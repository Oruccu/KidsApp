import React from 'react';
import { View, Text, SafeAreaView, ScrollView } from 'react-native';
import Background from '@/app/components/background';
import { useSelector } from 'react-redux';
import { useRoute } from '@react-navigation/native';
import { stories } from '@/app/data/stories';
import Banner from '@/app/components/Banner';
import styles from './styles';
import color from '@/app/styles/color';

const Story = () => {
  const currentMode = useSelector((state) => state.mode.currentMode);
  const route = useRoute();

  // Gelen id varsa al, yoksa varsayılan "theLittlePrince" hikayesini seç
  const storyId = route.params?.storyId || 'theLittlePrince';
  const currentStory = stories[storyId];

  const BackgroundColor = currentMode === 'Boy' ? color.BoyColors.Primary : color.GirlColors.Primary;

  if (!currentStory) {
    // Eğer geçerli hikaye hala yoksa (tanımsızsa), hata göster
    return (
      <Background showOverlay={true}>
        <SafeAreaView style={styles.container}>
          <Text style={styles.errorText}>Story not found.</Text>
        </SafeAreaView>
      </Background>
    );
  }

  const { title, text } = currentStory;

  return (
    <Background showOverlay={true}>
      <SafeAreaView style={styles.container}>
        <Banner bannerTitle={title} />
        <View style={[styles.storyContainer, { backgroundColor: BackgroundColor }]}>
          <ScrollView>
            <Text style={styles.story}>{text}</Text>
          </ScrollView>
        </View>
      </SafeAreaView>
    </Background>
  );
};

export default Story;
